using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;

public class UnitBuildablePanel : MonoBehaviour
{
    public Transform unitParent;
    public UnitsBuildableItem unit;
    
    [HideInInspector]
    public UnitsBuildableItem currentSelectedUnit;

    public void Init()
    {
        // 清理
        foreach (Transform child in unitParent)
        {
            Destroy(child.gameObject);
        }
        
        // 己方单位
        foreach (var unitSo in GameManager.RunTimeData.playerUnits)
        {
            // if(unitSo.entityType == EntityType.Building) continue;
            
            UnitsBuildableItem unitUI = Instantiate(unit, unitParent);
            unitUI.Setup(unitSo, this);
        }

        // 敌方单位，测试用
        // foreach (var enemyUnitSo in GameManager.RunTimeData.enemyUnits)
        // {
        //     UnitsBuildableItem unitUI = Instantiate(unit, unitParent);
        //     unitUI.Setup(enemyUnitSo, this);
        // }
    }
}
