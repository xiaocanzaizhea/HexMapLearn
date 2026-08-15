using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.Serialization;
using UnityEngine.UI;

public class UnitsBuildableItem : MonoBehaviour
    , IPointerClickHandler, IDragHandler, IEndDragHandler, IBeginDragHandler
{
    public Image image;
    public TextMeshProUGUI text;
    public GameObject highLight;
    public TextMeshProUGUI cntUI;
    [HideInInspector]
    public HexUnitDataSO dataSo;
    
    private GameObject obj; // 跟随鼠标移动的临时物体

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

    public void Setup(HexUnitDataSO hexUnitSo, UnitBuildablePanel unitBuildablePanel)
    {
        this.dataSo = hexUnitSo;
        this.text.text = hexUnitSo.name;
        this.image.sprite = hexUnitSo.icon;
        this.cnt = 0;
    }

    public void OnBeginDrag(PointerEventData eventData)
    {
        obj = new GameObject("GragGhost");
        obj.transform.SetParent(GetComponentInParent<Canvas>().transform);
        obj.transform.SetAsLastSibling();
        
        var ghostImage = obj.AddComponent<Image>();
        var sprite = dataSo.icon;
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
            UnitsBuildableItem currentSelectedUnit = GameManager.RunTimeData.CurrentSelectedUnitInUI;

            if (currentSelectedUnit == this) //两次点击
            {
                // EventManager.Instance.TriggerUnitUnSelection();
                currentSelectedUnit.RemoveSelected();
                GameManager.RunTimeData.CurrentSelectedUnitInUI = null;
                return;
            }
            
            if (currentSelectedUnit != null)
            {
                currentSelectedUnit.RemoveSelected();
            }
            
            GameManager.RunTimeData.CurrentSelectedUnitInUI = this;
            SetSelected();
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
