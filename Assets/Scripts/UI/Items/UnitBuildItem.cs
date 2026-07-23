using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.Serialization;
using UnityEngine.UI;

public class UnitBuildItem : MonoBehaviour, IPointerClickHandler
{
    public Image unitImage;
    public TextMeshProUGUI timeUI;
    [FormerlySerializedAs("unitDataEntity")] [FormerlySerializedAs("unitSo")] [HideInInspector]
    public UnitDataEntity playerUnitDataEntity;
    [FormerlySerializedAs("unitBuildList")] public UnitBuildPanel unitBuildPanel;

    public int Time
    {
        get => time;
        set
        {
            time = value;
            timeUI.text = value.ToString();
        }
    }
    private int time;
    
    public void OnPointerClick(PointerEventData eventData)
    {
        if (eventData.button == PointerEventData.InputButton.Right)
        {
            unitBuildPanel.RemoveUnitBuild(this);
        }
    }
}
