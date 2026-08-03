using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

[CreateAssetMenu(menuName = "SO/HexUnit", fileName = "HexUnitDataSO")]
public class HexUnitDataSO : ScriptableObject
{
    public string id;
    public Sprite sprite;
    public HexUnit prefab;
    public int maxhp;
    public int attack;
    public int defense;
    public int moveRange;
    public int sightRange;
    
    public int unitValue = 25;
    public int unitOccupancySize = 1;
    public int unitBuildTimeRequired = 2;
    public int retreatTimeRequired = 3;
    
    public CampType campType;
}

public enum CampType
{
    Enemy, Player
}
