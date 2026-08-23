using UnityEngine;
using UnityEditor;
using System.IO;
using System.Collections.Generic;

public class TextureBatchProcessor : EditorWindow
{
    private Vector2 scrollPosition;
    private List<Texture2D> selectedTextures = new List<Texture2D>();
    private TextureImporterType targetType = TextureImporterType.Sprite;
    private bool compressTextures = false;
    private int maxSize = 2048;
    private bool generateMipmaps = false;
    private TextureImporterFormat format = TextureImporterFormat.ASTC_8x8;
    private bool isReadable = false;

    [MenuItem("Tools/批量纹理处理器")]
    public static void ShowWindow()
    {
        GetWindow<TextureBatchProcessor>("批量纹理处理器");
    }

    private void OnGUI()
    {
        GUILayout.Label("批量设置纹理类型", EditorStyles.boldLabel);
        EditorGUILayout.Space();

        // ============ 选择设置 ============
        GUILayout.Label("目标类型", EditorStyles.boldLabel);
        targetType = (TextureImporterType)EditorGUILayout.EnumPopup("纹理类型", targetType);
        EditorGUILayout.Space();

        // ============ 高级设置 ============
        GUILayout.Label("高级设置", EditorStyles.boldLabel);
        compressTextures = EditorGUILayout.Toggle("压缩纹理", compressTextures);
        maxSize = EditorGUILayout.IntPopup(
            new GUIContent("最大尺寸"), 
            maxSize, 
            new GUIContent[] { 
                new GUIContent("32"), 
                new GUIContent("64"), 
                new GUIContent("128"), 
                new GUIContent("256"), 
                new GUIContent("512"), 
                new GUIContent("1024"), 
                new GUIContent("2048"), 
                new GUIContent("4096") 
            }, 
            new int[] { 32, 64, 128, 256, 512, 1024, 2048, 4096 }
        );
        generateMipmaps = EditorGUILayout.Toggle("生成Mipmaps", generateMipmaps);
        isReadable = EditorGUILayout.Toggle("可读（Read/Write）", isReadable);
        format = (TextureImporterFormat)EditorGUILayout.EnumPopup("压缩格式", format);
        EditorGUILayout.Space();

        // ============ 选择文件 ============
        GUILayout.Label("选择纹理", EditorStyles.boldLabel);
        EditorGUILayout.BeginHorizontal();
        if (GUILayout.Button("从文件夹选择", GUILayout.Height(30)))
        {
            string path = EditorUtility.OpenFolderPanel("选择纹理文件夹", Application.dataPath, "");
            if (!string.IsNullOrEmpty(path))
            {
                LoadTexturesFromFolder(path);
            }
        }
        if (GUILayout.Button("从Project选择", GUILayout.Height(30)))
        {
            LoadTexturesFromProject();
        }
        EditorGUILayout.EndHorizontal();

        EditorGUILayout.Space();

        // ============ 显示选择列表 ============
        EditorGUILayout.LabelField($"已选纹理: {selectedTextures.Count}");
        scrollPosition = EditorGUILayout.BeginScrollView(scrollPosition, GUILayout.Height(200));
        foreach (var tex in selectedTextures)
        {
            EditorGUILayout.ObjectField(tex, typeof(Texture2D), false);
        }
        EditorGUILayout.EndScrollView();

        EditorGUILayout.Space();

        // ============ 执行按钮 ============
        GUI.enabled = selectedTextures.Count > 0;
        if (GUILayout.Button($"批量处理 ({selectedTextures.Count} 个纹理)", GUILayout.Height(40)))
        {
            ProcessTextures();
        }
        GUI.enabled = true;

        if (GUILayout.Button("清空列表", GUILayout.Height(25)))
        {
            selectedTextures.Clear();
        }
    }

    // ============ 加载纹理 ============
    private void LoadTexturesFromFolder(string folderPath)
    {
        // 获取相对于项目的路径
        string relativePath = GetRelativePath(folderPath);
    
        // 搜索所有子文件夹里的图片
        string[] files = Directory.GetFiles(folderPath, "*.*", SearchOption.AllDirectories);

        selectedTextures.Clear();

        foreach (string file in files)
        {
            // 检查文件扩展名
            if (file.EndsWith(".png") || file.EndsWith(".jpg") || file.EndsWith(".jpeg") || file.EndsWith(".tga"))
            {
                // 修复：获取相对于文件夹的路径，而不是只取文件名
                string relativeFilePath = GetRelativeFilePath(folderPath, file);
                string assetPath = relativePath + "/" + relativeFilePath;
            
                // 修正路径格式（把反斜杠转成正斜杠）
                assetPath = assetPath.Replace("\\", "/");
            
                Texture2D tex = AssetDatabase.LoadAssetAtPath<Texture2D>(assetPath);
                if (tex != null)
                {
                    selectedTextures.Add(tex);
                }
                else
                {
                    Debug.LogWarning($"无法加载纹理: {assetPath}");
                }
            }
        }

        Debug.Log($"从文件夹加载了 {selectedTextures.Count} 个纹理");
    }

// 辅助方法：获取相对于根文件夹的路径
    private string GetRelativeFilePath(string rootFolder, string fullPath)
    {
        // 去掉根文件夹路径，保留子文件夹结构
        string relativePath = fullPath.Substring(rootFolder.Length);
    
        // 去掉开头的路径分隔符
        if (relativePath.StartsWith("\\") || relativePath.StartsWith("/"))
        {
            relativePath = relativePath.Substring(1);
        }
    
        return relativePath;
    }

    private void LoadTexturesFromProject()
    {
        string[] guids = Selection.assetGUIDs;
        selectedTextures.Clear();

        foreach (string guid in guids)
        {
            string path = AssetDatabase.GUIDToAssetPath(guid);
            Texture2D tex = AssetDatabase.LoadAssetAtPath<Texture2D>(path);
            if (tex != null)
            {
                selectedTextures.Add(tex);
            }
        }

        Debug.Log($"从Project加载了 {selectedTextures.Count} 个纹理");
    }

    // ============ 核心处理 ============
    private void ProcessTextures()
    {
        int successCount = 0;
        int failCount = 0;

        AssetDatabase.StartAssetEditing();

        foreach (var tex in selectedTextures)
        {
            string path = AssetDatabase.GetAssetPath(tex);
            TextureImporter importer = AssetImporter.GetAtPath(path) as TextureImporter;

            if (importer == null)
            {
                Debug.LogWarning($"无法处理: {path}");
                failCount++;
                continue;
            }

            try
            {
                // 设置纹理类型
                importer.textureType = targetType;

                // 根据类型设置特定参数
                if (targetType == TextureImporterType.Sprite)
                {
                    importer.spriteImportMode = SpriteImportMode.Single;
                    importer.spritePixelsPerUnit = 100;
                }
                else if (targetType == TextureImporterType.NormalMap)
                {
                    importer.textureType = TextureImporterType.NormalMap;
                }
                else if (targetType == TextureImporterType.GUI)
                {
                    importer.textureType = TextureImporterType.GUI;
                }

                // 设置平台设置
                TextureImporterPlatformSettings platformSettings = importer.GetDefaultPlatformTextureSettings();
                platformSettings.maxTextureSize = maxSize;
                platformSettings.format = format;
                platformSettings.resizeAlgorithm = TextureResizeAlgorithm.Mitchell;
                platformSettings.textureCompression = compressTextures ? TextureImporterCompression.Compressed : TextureImporterCompression.Uncompressed;

                // 应用设置
                importer.mipmapEnabled = generateMipmaps;
                importer.isReadable = isReadable;
                importer.filterMode = FilterMode.Bilinear;
                importer.anisoLevel = 1;

                // 保存
                importer.SaveAndReimport();
                successCount++;
            }
            catch (System.Exception e)
            {
                Debug.LogError($"处理失败: {path} - {e.Message}");
                failCount++;
            }
        }

        AssetDatabase.StopAssetEditing();
        AssetDatabase.Refresh();

        Debug.Log($"✅ 批量处理完成！成功: {successCount}, 失败: {failCount}");
        EditorUtility.DisplayDialog("完成", $"批量处理完成！\n成功: {successCount}\n失败: {failCount}", "确定");
    }

    // ============ 辅助方法 ============
    private string GetRelativePath(string fullPath)
    {
        string dataPath = Application.dataPath;
        if (fullPath.StartsWith(dataPath))
        {
            return "Assets" + fullPath.Substring(dataPath.Length);
        }
        return fullPath;
    }
}