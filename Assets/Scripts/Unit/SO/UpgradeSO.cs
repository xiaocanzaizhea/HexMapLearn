using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "SO/Upgrade")]
public class UpgradeSO : ScriptableObject
{
    public UpgradeType type;
    public string upgradeName;
    [TextArea] public string description;
    public int maxLevel = 5;
    public int[] costs;     // 每级花费
    public int[] values;    // 每级数值
    public Sprite icon;
}

public enum UpgradeType
{
    AttackPower,    // 攻击力
    Defense,        // 防御力
    MaxHealth,      // 最大生命
    Sanity,         // 精神力
    VisionRange,    // 视野
    ExpBonus,       // 经验加成
}
