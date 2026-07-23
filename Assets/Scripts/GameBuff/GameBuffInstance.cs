using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class GameBuffInstance
{
    public int buffId;
    public float passedLimit; 
    public float timer;
    public GameBuffDataEntity GetBuffEntity() 
    {
        return GameManager.Instance.GameBuffData[buffId];
    }
}

