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
        backToVillageBtn.onClick.AddListener(() =>
        {
            BackToVillage();
        });
    }

    void BackToVillage()
    {
        // GameManager.Scene.
    }
}
