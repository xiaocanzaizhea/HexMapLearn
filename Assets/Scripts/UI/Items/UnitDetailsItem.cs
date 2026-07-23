using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UnitDetailsItem : MonoBehaviour
{
    public Image itemImage;
    public TextMeshProUGUI itemName;

    public void Setup(Sprite sprite, string name)
    {
        this.itemImage.sprite = sprite;
        this.itemName.text = name;
    }
}
