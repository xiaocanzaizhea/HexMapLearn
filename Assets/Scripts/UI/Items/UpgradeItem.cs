using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UpgradeItem : MonoBehaviour
{
    public TextMeshProUGUI nameText;
    public TextMeshProUGUI levelText;
    public TextMeshProUGUI costText;
    public Image upgradeIcon;
    public Button button;

    private UpgradeSO data;
    private Action onClick;

    public void Setup(UpgradeSO upgrade, Action callback)
    {
        data = upgrade;
        onClick = callback;
        button.onClick.AddListener(() => onClick?.Invoke());
        Refresh(PlayerPrefs.GetInt($"Upgrade_{data.type}", 0));
    }

    public void Refresh(int currentLevel)
    {
        nameText.text = data.upgradeName;
        levelText.text = $"Lv.{currentLevel}/{data.maxLevel}";
        upgradeIcon.sprite = data.icon;

        if (currentLevel >= data.maxLevel)
        {
            costText.text = "Max Level";
            button.interactable = false;
        }
        else
        {
            costText.text = $"Cost: {data.costs[currentLevel]}";
            button.interactable = true;
        }
    }
}
