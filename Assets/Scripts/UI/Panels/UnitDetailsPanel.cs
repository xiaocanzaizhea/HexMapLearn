using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UnitDetailsPanel : BasePanel
{
    public TextMeshProUGUI unitName;
    public Image unitImage;
    public TextMeshProUGUI unitTarget;
    public Slider unitHealth;

    public Transform attributeParent;
    public Transform abilityParent;
    
    public UnitDetailsItem unitDetails_Item;
    
    public Button retreatButton;
    public TextMeshProUGUI buttonText;
    
    public List<Color> levelImageColors;

    public override void Init()
    {
        
    }
    
    void UpdateUnitDetails(HexUnit unit, bool isMapUnit)
    {
        this.unitName.text = unit.Id;
        this.unitImage.sprite = unit.Icon;
        
        this.unitTarget.text = isMapUnit ? "Map Unit" : "Unit";
        SetupAttributes(unit, isMapUnit);

        if (isMapUnit)
        {
            this.unitHealth.gameObject.SetActive(true);
            this.unitHealth.value = (float)unit.Health / unit.dataSo.maxhp;
        }
        else
        {
            this.unitHealth.gameObject.SetActive(false);
        }

        if (unit is HexUnit playerUnit)
        {
            SetupPlayerUnit(playerUnit, isMapUnit);
        }
    }

    // 处理真实单位
    void SetupAttributes(HexUnit unit, bool isMapUnit)
    {
        // 先清理
        foreach (Transform child in attributeParent) Destroy(child.gameObject);

        int attack = isMapUnit ? unit.AttackPower : unit.dataSo.attack;
        int defense = isMapUnit ? unit.Defense : unit.dataSo.defense;
        
        // 攻击图标
        AddAttributeItem(GameResource.Instance.attackImage, attack.ToString());
        // 防御图标
        AddAttributeItem(GameResource.Instance.defenseImage, defense.ToString());
        // 精神力图标
        // AddAttributeItem(GameResource.Instance.sanityImage, sanity.ToString());
    }

    void SetupPlayerUnit(HexUnit unit, bool isMapUnit)
    {
        var retreatTimeRequired = unit.dataSo.retreatTimeRequired;
        if (!isMapUnit)
        {
            retreatButton.gameObject.SetActive(false);
            return;
        }

        retreatButton.gameObject.SetActive(true);
        if (retreatTimeRequired == -1)
        {
            SetButton("Retreat", () => unit.StartRetreat());
        }
        else
        {
            SetButton("CancelRetreat", () => unit.CancelRetreat());
        }
    }

    public void SetButton(string label, Action action)
    {
        buttonText.text = label;
        retreatButton.onClick.RemoveAllListeners();
        retreatButton.onClick.AddListener(() => action?.Invoke());
    }
    
    void AddAttributeItem(Image image, String text)
    {
        var item = Instantiate(unitDetails_Item, attributeParent);
        item.itemImage = image;
        item.itemName.text = text;
    }
}
