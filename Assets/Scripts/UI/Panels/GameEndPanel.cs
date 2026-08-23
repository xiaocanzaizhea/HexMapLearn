using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameEndPanel : BasePanel
{
    public TextMeshProUGUI titleText;
    public TextMeshProUGUI detailText;
    public Button backToVillageBtn;

    public override void Init()
    {
        titleText.text = "Game End";
        detailText.text = "";
        GameManager.UI.HidePanel<PlayerMainInfoPanel>();
        backToVillageBtn.onClick.AddListener(() =>
        {
            BackToVillage();
        });
    }

    async void BackToVillage()
    {
        SceneLoadPanel panel = await GameManager.UI.ShowPanel<SceneLoadPanel>();
        GameManager.UI.mainCanvas.PrepareFade();
        await GameManager.AssetLoader.LoadScene("VillageScene", p =>
        {
            panel.SetPercentage((int)p*100);
        }, scene =>
        {
            panel.SetPercentage(100);
            GameManager.UI.HidePanel<GameEndPanel>();
        });
    }
}
