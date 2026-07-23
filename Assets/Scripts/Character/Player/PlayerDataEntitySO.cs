using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[CreateAssetMenu(fileName = "New PlayerData",menuName = "ScriptableObject/PlayerData")]
public class PlayerDataEntitySO : ScriptableObject
{
    public PlayerDataInstance GetInstance()
    {
        return new PlayerDataInstance();
    }
}
