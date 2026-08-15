using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.Serialization;

[CreateAssetMenu(menuName = "SO/HexUnit", fileName = "HexUnitDataSO")]
public class HexUnitDataSO : ScriptableObject
{
    public string id;
    [FormerlySerializedAs("sprite")] public Sprite icon;
    public HexUnit prefab;
    public UnitTeam team = UnitTeam.Player;              // ← 阵营
    public Color teamColor = Color.white;
    
    [Header("基础属性")]
    public int maxhp;
    public int defense;
    
    [Header("战斗属性")]
    public bool canAttack = true;
    public int attack = 10;
    public int attackRange = 1;

    [Header("移动属性")]
    public bool canMove = true;
    public int moveRange = 2;

    [Header("视野属性")] 
    public bool canView = true;
    public int sightRange = 2;
    
    // 以下属性只有玩家单位才有
    public int unitValue = 25;
    public int unitOccupancySize = 1;
    public int unitBuildTimeRequired = 2;
    public int retreatTimeRequired = 3;
    
}

public enum UnitTeam
{
    Player,
    Enemy
}
