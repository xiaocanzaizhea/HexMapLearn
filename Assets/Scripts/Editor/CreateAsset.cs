using System.Collections;
using System.Collections.Generic;
using System.IO;
using OfficeOpenXml;
using UnityEditor;
using UnityEngine;

public class CreateAsset : UnityEditor.Editor
{
    private enum ExcelTitleEnum
    {
        Id,
        Name,
        Hp,
        ATK,
        Speed,
        IconPath
    }

    private enum PlayerEnum
    {
        id,
        maxHp,
        attack,
        defense,
        moveRange,
        sightRange,
        unitValue,
        unitOccupancySize,
        unitBuildTimeRequired,
        retreatTimeRequired,
        icon
    }
    
    [MenuItem("Tools/CreateAssetFromExcel/CreatePlayerAsset")]
    static void CreatePlayerAssetFromExcel()
    {
        string excelPath = Path.Combine(Application.dataPath, "Scripts/Editor/DataSO.xlsx");
    
        if (!File.Exists(excelPath))
        {
            return;
        }
    
        using var package = new ExcelPackage(new FileInfo(excelPath));
    
        foreach (var worksheet in package.Workbook.Worksheets)
        {
            CreatePlayerScriptableObject(worksheet);
        }
    
        AssetDatabase.Refresh();
    }

    private static void CreatePlayerScriptableObject(ExcelWorksheet worksheet)
    {
        PlayerUnitsData playerUnitsData = CreateInstance<PlayerUnitsData>();
        int startRow = 2, startCol = 1;
        for (int i = startRow; i <= worksheet.Dimension.Rows; i++)
        {
            PlayerUnitDataEntity playerUnitDataEntity = CreateInstance<PlayerUnitDataEntity>();
            playerUnitDataEntity.id = worksheet.Cells[i, startCol].Value.ToString();
            playerUnitDataEntity.maxhp = int.Parse(worksheet.Cells[i, startCol+(int)PlayerEnum.maxHp].Text);
            playerUnitDataEntity.attack = int.Parse(worksheet.Cells[i, startCol+(int)PlayerEnum.attack].Text);
            playerUnitDataEntity.defense = int.Parse(worksheet.Cells[i, startCol+(int)PlayerEnum.defense].Text);
            playerUnitDataEntity.moveRange = int.Parse(worksheet.Cells[i, startCol+(int)PlayerEnum.moveRange].Text);
            playerUnitDataEntity.sightRange = int.Parse(worksheet.Cells[i, startCol+(int)PlayerEnum.sightRange].Text);
            playerUnitDataEntity.unitValue = int.Parse(worksheet.Cells[i, startCol+(int)PlayerEnum.unitValue].Text);
            playerUnitDataEntity.unitOccupancySize = int.Parse(worksheet.Cells[i, startCol+(int)PlayerEnum.unitOccupancySize].Text);
            playerUnitDataEntity.unitBuildTimeRequired = int.Parse(worksheet.Cells[i, startCol+(int)PlayerEnum.unitBuildTimeRequired].Text);
            playerUnitDataEntity.retreatTimeRequired = int.Parse(worksheet.Cells[i, startCol+(int)PlayerEnum.retreatTimeRequired].Text);
        
            string iconPath = worksheet.Cells[i, startCol+(int)PlayerEnum.icon].Text;
            playerUnitDataEntity.sprite = AssetDatabase.LoadAssetAtPath<Sprite>(iconPath);

            string fileName = playerUnitDataEntity.id;
            string savePath = $"Assets/AddressableAssets/SO/Player/{fileName}.asset";
            if (!Directory.Exists(Path.GetDirectoryName(savePath))) 
                Directory.CreateDirectory(Path.GetDirectoryName(savePath));
        
            AssetDatabase.CreateAsset(playerUnitDataEntity, savePath);
            AssetDatabase.SaveAssets();
            
            playerUnitsData.data.Add(playerUnitDataEntity);
        }
        
        string savePath2 = "Assets/AddressableAssets/SO/Player/PlayerUnitsData.asset";
        AssetDatabase.CreateAsset(playerUnitsData, savePath2);
        AssetDatabase.SaveAssets();
    }

    [MenuItem("Tools/CreateAssetFromExcel/TestExample")]
    static void CreateAssetFromExcel()
    {
        string excelPath = Path.Combine(Application.dataPath, "Scripts/Editor/EnemyData.xlsx");
    
        if (!File.Exists(excelPath))
        {
            return;
        }
    
        using var package = new ExcelPackage(new FileInfo(excelPath));
    
        foreach (var worksheet in package.Workbook.Worksheets)
        {
            CreateScriptableObject(worksheet);
        }
    
        AssetDatabase.Refresh();
    }

    private static void CreateScriptableObject(ExcelWorksheet worksheet)
    {
        EnemiesData enemiesData = CreateInstance<EnemiesData>();
        int startRow = 2, startCol = 1;
        for (int i = startRow; i <= worksheet.Dimension.Rows; i++)
        {
            EnemyDataEntity enemyDataEntity = CreateInstance<EnemyDataEntity>();

            enemyDataEntity.Id = worksheet.Cells[i, startCol+(int)ExcelTitleEnum.Id].Text;
            enemyDataEntity.Name = worksheet.Cells[i, startCol+(int)ExcelTitleEnum.Name].Text;
            enemyDataEntity.HP = int.Parse(worksheet.Cells[i, startCol+(int)ExcelTitleEnum.Hp].Text);
            enemyDataEntity.Speed = float.Parse(worksheet.Cells[i, startCol+(int)ExcelTitleEnum.Speed].Text);
            enemyDataEntity.ATK = int.Parse(worksheet.Cells[i, startCol+(int)ExcelTitleEnum.ATK].Text);

            string iconPath = worksheet.Cells[i, startCol+(int)ExcelTitleEnum.IconPath].Text;
            enemyDataEntity.Sprite = AssetDatabase.LoadAssetAtPath<Sprite>(iconPath);
        
            string fileName = Path.GetFileNameWithoutExtension(iconPath);
        
            string savePath = $"Assets/AddressableAssets/SO/Enemy/{fileName}.asset";
            if (!Directory.Exists(Path.GetDirectoryName(savePath))) 
                Directory.CreateDirectory(Path.GetDirectoryName(savePath));
        
            AssetDatabase.CreateAsset(enemyDataEntity, savePath);
            AssetDatabase.SaveAssets();
            
            enemiesData.data.Add(enemyDataEntity);
        }
        string savePath2 = "Assets/AddressableAssets/SO/Enemy/EnemiesData.asset";
        AssetDatabase.CreateAsset(enemiesData, savePath2);
        AssetDatabase.SaveAssets();
    }
}
