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

    private void OnEnable()
    {
        GameManager.Event.Register(HexEvents.NextRound.ToString(), new GameEvent(OnNextRound));
    }

    private void OnDisable()
    {
        GameManager.Event.Unregister(HexEvents.NextRound.ToString(), new GameEvent(OnNextRound));
    }

    void OnNextRound()
    {
        if (unitBuildList.Count > 0)
        {
            var unitBuildItem = unitBuildList[0];
            if (unitBuildItem.Time <= 0)
            {
                if(unitBuildItem.dataSo.unitValue > GameManager.RunTimeData.ResourceCount) return;
                GameManager.Event.Broadcast(HexEvents.ResourceChange.ToString(),
                    new GameEventParameter<int>(-unitBuildItem.dataSo.unitValue));
                GameManager.Event.Broadcast(HexEvents.UnitBuildSuccess.ToString(),
                    new GameEventParameter<string>(unitBuildItem.dataSo.id));
                unitBuildList.RemoveAt(0);
                Destroy(cloneParent.GetChild(0).gameObject);
            }
            else
            {
                unitBuildItem.Time -= 1;
            }
        }
    }

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

        OnUnitDroppedToList(unitsBarUnit.dataSo);
    }
    
    private void OnUnitDroppedToList(HexUnitDataSO dataSo)
    {
        UnitBuildItem ub = Instantiate(cloneTarget, cloneParent);
        if(dataSo.icon != null) ub.unitImage.sprite = dataSo.icon;
        ub.dataSo = dataSo;
        ub.Time = dataSo.unitBuildTimeRequired;
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
