using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class GameBuffDataEntity
{
    public int Id;
    public BuffType Type;
    public BuffTarget Target;
    public string Description;
    public float LimitValue;
    public float Interval;
    public int Value;

    public GameBuffInstance GetInstance() 
    {
        return new GameBuffInstance()
        {
            buffId = Id,
            timer = 0,
            passedLimit = 0,
        };
    }
}
public enum BuffType
{
    TimeLimit,
    NumericalLimit,
    NoLimit
}
public enum BuffTarget 
{
    HP,
    MaxHP,
    MP,
    MaxMP,
    EP,
    MaxEP,
    ATK,
    DEF,
    SPD,
    Buff
}
