using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class UnitBuildPanel : MonoBehaviour, IDropHandler
{
    public Transform cloneParent;
    public UnitBuildItem cloneTarget;
    
    private List<UnitBuildItem> unitBuildList = new List<UnitBuildItem>();

    public void Init()
    {
        foreach (Transform child in cloneParent)
        {
            Destroy(child.gameObject);
        }
    }

    public void OnDrop(PointerEventData eventData)
    {
        var draggedObject = eventData.pointerDrag;
        if(draggedObject == null) return;

        var unitsBarUnit = draggedObject.GetComponent<UnitsBuildableItem>();
        if(unitsBarUnit == null) return;

        OnUnitDroppedToList(unitsBarUnit.playerUnitDataEntity);
    }
    
    private void OnUnitDroppedToList(PlayerUnitDataEntity playerUnitDataEntity)
    {
        UnitBuildItem ub = Instantiate(cloneTarget, cloneParent);
        if(playerUnitDataEntity.sprite != null) ub.unitImage.sprite = playerUnitDataEntity.sprite;
        ub.playerUnitDataEntity = playerUnitDataEntity;
        ub.Time = playerUnitDataEntity.unitBuildTimeRequired;
        ub.unitBuildPanel = this;
        
        unitBuildList.Add(ub);
    }

    public void RemoveUnitBuild(UnitBuildItem unitBuildItem)
    {
        unitBuildList.Remove(unitBuildItem);
        // 清除UI
        foreach (Transform child in cloneParent)
        {
            var unitBuildComponent = child.GetComponent<UnitBuildItem>();
            if (unitBuildComponent == unitBuildItem)
            {
                Destroy(child.gameObject);
                break; // 找到了就退出
            }
        }
    }
}
