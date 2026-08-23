using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Map;
using UnityEngine;

public class GameRunTimeManager
{
    public HexGrid grid;

    public int maxUnitCount = 100;
    // 对局中可放置单位
    public List<HexUnitDataSO> playerUnits = new List<HexUnitDataSO>();
    // 对局中可能生成的敌方单位
    public List<HexUnitDataSO> enemyUnits = new List<HexUnitDataSO>();
    
    // 开始赠送的资源数目
    public int startResourceCount = 100;
    // 最大资源数
    public int maxResourceCount = 1000;
    
    // 每回合最大行动次数
    public int playerMaxActionCount;
    
    public UnitsBuildableItem CurrentSelectedUnitInUI
    {
        get;
        set;
    }

    public HexUnit CurrentUnitInMap
    {
        get;
        set;
    }

    public int enemyUnitStartCount = 1;
    
    // 玩家资源数
    public int ResourceCount
    {
        get => resourceCount;
        set => resourceCount = value;
    }
    private int resourceCount;
    
    // 玩家单位拥有数
    public int UnitCount
    {
        get => unitCount;
        set => unitCount = value;
    }
    private int unitCount;

    void OnGameStart()
    {
        ResourceCount += startResourceCount;
    }

    public GameRunTimeManager()
    {
        
    }
}
