using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "PlayerDataGroup_", menuName = "DataSO/CreatePlayerGroup")]
public class PlayerUnitsData : ScriptableObject
{
    public List<PlayerUnitDataEntity> data = new List<PlayerUnitDataEntity>();
}
