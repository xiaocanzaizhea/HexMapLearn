using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UpgradePanel : BasePanel
{
    public UpgradeSO[] upgrades;
    public UpgradeItem[] buttons;

    private void Start()
    {
        for (int i = 0; i < upgrades.Length; i++)
        {
            int index = i;
            buttons[i].Setup(upgrades[i], () => Upgrade(index));
        }
    }

    void Upgrade(int index)
    {
        var upgrade = upgrades[index];
        int currentLevel = PlayerPrefs.GetInt($"Upgrade_{upgrade.type}", 0);
        
        if (currentLevel >= upgrade.maxLevel) return; // 满级

        int cost = upgrade.costs[currentLevel];
        // if (GameManager.Instance.ResourceCount < cost) return; // 不够钱

        // 扣钱
        // GameManager.Instance.ResourceCount -= cost;
        
        // 升级
        currentLevel++;
        PlayerPrefs.SetInt($"Upgrade_{upgrade.type}", currentLevel);
        
        // 刷新按钮
        buttons[index].Refresh(currentLevel);
    }

    void Clearup()
    {
        foreach (var upgrade in upgrades)
        {
            PlayerPrefs.SetInt($"Upgrade_{upgrade.type}", 0);
        }
    }

    public override void Init()
    {
        
    }
}
