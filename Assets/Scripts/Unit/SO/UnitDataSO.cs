using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UnitDataSO : ScriptableObject
{
    public string id;
    public Sprite sprite;
    public int maxhp;
    public int attack;
    public int defense;
    public int moveRange;
    public int sightRange;
    public GameObject gameObject;
}
