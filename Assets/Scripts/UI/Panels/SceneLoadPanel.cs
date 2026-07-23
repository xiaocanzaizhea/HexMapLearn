using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.ResourceManagement.ResourceProviders;

public class SceneLoadPanel : BasePanel
{
    private int percentage;
    public TMP_Text loadPercentageText;
    public TMP_Text promptText;
    public bool pressKeyToEnterScene = true;

    public SceneInstance sceneInstance;
    private static string constString = " / 100  Loaded...";
    public override void Init()
    {
    }

    public override void Show()
    {
        base.Show();
        GameManager.UI.mainCanvas.PrepareFade();
    }

    public void SetPercentage(int percentage) 
    {
        this.percentage = percentage;
        loadPercentageText.text = percentage.ToString() + constString;
    }

    protected override void Update()
    {
        base.Update();
        if (!isShow) return;
        if (percentage >= 100) 
        {
            percentage = 100;
			
            if (pressKeyToEnterScene)
            {
                promptText.gameObject.SetActive(true);
                if (Input.GetKeyDown(KeyCode.Space) && promptText.gameObject.activeSelf)
                {
                    GameManager.UI.HidePanel<SceneLoadPanel>(() => 
                    {
                        GameManager.UI.mainCanvas.StartFade();
                        GameManager.Instance.readyToActiveLoadedScene = true;
                    });
                    percentage = 0;
                }
            }
            else 
            {
                GameManager.UI.HidePanel<SceneLoadPanel>(() =>
                {
                    GameManager.UI.mainCanvas.StartFade();
                    GameManager.Instance.readyToActiveLoadedScene = true;
                    pressKeyToEnterScene = true;
                });
            }
        }
    }
}