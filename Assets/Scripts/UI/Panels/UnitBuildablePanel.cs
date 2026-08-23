using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.Serialization;

public class UnitBuildablePanel : MonoBehaviour
{
    public Transform unitParent;
    // 复制对象
    public UnitsBuildableItem unit;
    
    private Dictionary<string, UnitsBuildableItem> units = new Dictionary<string, UnitsBuildableItem>();
    
    [HideInInspector]
    public UnitsBuildableItem currentSelectedUnit;

    private void OnEnable()
    {
        GameManager.Event.Register(HexEvents.UnitBuildSuccess.ToString(), new GameEvent<string>(UnitBuildSuccess));
        GameManager.Event.Register(HexEvents.UnitSpawn.ToString(), new GameEvent<string>(OnUnitSpawn));
    }

    private void OnDisable()
    {
        GameManager.Event.Unregister(HexEvents.UnitBuildSuccess.ToString(), new GameEvent<string>(UnitBuildSuccess));
        GameManager.Event.Unregister(HexEvents.NextRound.ToString(), new GameEvent<string>(OnUnitSpawn));
    }

    public async void Init()
    {
        
        foreach (Transform child in unitParent)
        {
            Destroy(child.gameObject);
        }

        var unitDataList1 = await GameManager.AssetLoader.LoadAsset<UnitDataList>("PlayerDataList");
        // 己方单位
        foreach (var unitSo in unitDataList1.data)
        {
            UnitsBuildableItem unitUI = Instantiate(unit, unitParent);
            unitUI.Setup(unitSo, this);
            units.Add(unitSo.id, unitUI);
        }
        
        var unitDataList2 = await GameManager.AssetLoader.LoadAsset<UnitDataList>("EnemyDataList");
        // 敌人单位
        foreach (var unitSo in unitDataList2.data)
        {
            UnitsBuildableItem unitUI = Instantiate(unit, unitParent);
            unitUI.Setup(unitSo, this);
            units.Add(unitSo.id, unitUI);
        }
    }

    private void Start()
    {
        
    }

    public void UnitBuildSuccess(string unitName)
    {
        if (units.ContainsKey(unitName))
        {
            units[unitName].UnitCount += 1;
        }
    }
    
    void OnUnitSpawn(string id)
    {
        if (units.ContainsKey(id))
        {
            units[id].UnitCount -= 1;
        }
    }
}
