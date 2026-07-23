using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.Serialization;
using UnityEngine.UI;

public class UnitsBuildableItem : MonoBehaviour, 
    IDragHandler, IBeginDragHandler, IEndDragHandler, IPointerClickHandler, 
    IUnitBuildHandler, IUnitSpawnHandler
{
    public Image image;
    public TextMeshProUGUI text;
    public GameObject highLight;
    public TextMeshProUGUI cntUI;
    [FormerlySerializedAs("unitDataEntity")] [FormerlySerializedAs("unitSo")] [HideInInspector]
    public UnitDataEntity playerUnitDataEntity;
    
    private GameObject obj; // 跟随鼠标移动的临时物体
    
    private UnitBuildablePanel _unitBuildablePanel;

    public int UnitCount
    {
        get
        {
            return cnt;
        }
        set
        {
            cnt = value;
            cntUI.text = cnt.ToString();
        }
    }
    
    // 单位拥有数
    private int cnt;

    private void Awake()
    {
        highLight.GetComponent<Image>().enabled = true;
    }

    public void Setup(UnitDataEntity playerUnitDataEntity, UnitBuildablePanel unitBuildablePanel)
    {
        this.playerUnitDataEntity = playerUnitDataEntity;
        this.text.text = playerUnitDataEntity.name;
        this.image.sprite = playerUnitDataEntity.sprite;
        this._unitBuildablePanel = unitBuildablePanel;
        this.cnt = 0;
    }

    private void OnEnable()
    {
        EventManager.Instance.AddListener_UnitBuild(OnUnitBuild);
        EventManager.Instance.AddListener_UnitSpawn(OnUnitSpawn);
    }

    private void OnDisable()
    {
        EventManager.Instance.RemoveListener_UnitBuild(OnUnitBuild);
        EventManager.Instance.RemoveListener_UnitSpawn(OnUnitSpawn);
    }
    
    public void OnUnitBuild(HexUnit unit)
    {
        if (unit.playerUnitDataEntity == playerUnitDataEntity)
        {
            UnitCount += 1;
        }
    }
    
    public void OnUnitSpawn(HexUnit unit)
    {
        if (unit.playerUnitDataEntity == playerUnitDataEntity)
        {
            UnitCount -= 1;
        }
    }

    public void OnBeginDrag(PointerEventData eventData)
    {
        obj = new GameObject("GragGhost");
        obj.transform.SetParent(GetComponentInParent<Canvas>().transform);
        obj.transform.SetAsLastSibling();
        
        var ghostImage = obj.AddComponent<Image>();
        var sprite = playerUnitDataEntity.sprite;
        if(sprite != null) ghostImage.sprite = sprite;
        ghostImage.raycastTarget = false; 
        
        obj.transform.position = Input.mousePosition;
    }
    
    public void OnDrag(PointerEventData eventData)
    {
        if(obj != null)
            obj.transform.position = Input.mousePosition;
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        if (obj != null)
        {
            Destroy(obj);
            obj = null;
        }
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        if (eventData.button == PointerEventData.InputButton.Left)
        {
            UnitsBuildableItem currentSelectedUnit = _unitBuildablePanel.currentSelectedUnit;

            if (currentSelectedUnit == this) //两次点击
            {
                EventManager.Instance.TriggerUnitUnSelection();
                currentSelectedUnit.RemoveSelected();
                _unitBuildablePanel.currentSelectedUnit = null;
                return;
            }
            
            if (currentSelectedUnit != null)
            {
                currentSelectedUnit.RemoveSelected();
            }
            
            _unitBuildablePanel.currentSelectedUnit = this;
            this.SetSelected();
            
            EventManager.Instance.TriggerUnitSelection(this.playerUnitDataEntity.prefab, false);
        }
    }
    
    public void SetSelected()
    {
        highLight.SetActive(false);
    }

    public void RemoveSelected()
    {
        highLight.SetActive(true);
    }
}
