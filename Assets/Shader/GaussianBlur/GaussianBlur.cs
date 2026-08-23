using UnityEngine;

public class GaussianBlur : MonoBehaviour
{
    public Shader blurShader;
    [Range(0, 5)]
    public int blurSize = 1;

    private Material blurMaterial;

    void Start()
    {
        if (blurShader == null)
        {
            blurShader = Shader.Find("Custom/GaussianBlur");
        }

        blurMaterial = new Material(blurShader);
    }

    void OnRenderImage(RenderTexture src, RenderTexture dst)
    {
        if (blurMaterial == null)
        {
            Graphics.Blit(src, dst);
            return;
        }

        blurMaterial.SetFloat("_BlurSize", blurSize);

        // 创建临时渲染纹理
        RenderTexture temp1 = RenderTexture.GetTemporary(src.width, src.height, 0);
        RenderTexture temp2 = RenderTexture.GetTemporary(src.width, src.height, 0);

        // Pass 0: 水平模糊 → temp1
        Graphics.Blit(src, temp1, blurMaterial, 0);

        // Pass 1: 垂直模糊 → temp2
        Graphics.Blit(temp1, temp2, blurMaterial, 1);

        // 输出到屏幕
        Graphics.Blit(temp2, dst);

        // 释放临时纹理
        RenderTexture.ReleaseTemporary(temp1);
        RenderTexture.ReleaseTemporary(temp2);
    }

    void OnDestroy()
    {
        if (blurMaterial != null)
        {
            DestroyImmediate(blurMaterial);
        }
    }
}