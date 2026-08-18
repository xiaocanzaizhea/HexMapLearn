using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "SO/UnitDataList", fileName = "UnitDataList")]
public class UnitDataList : ScriptableObject
{
    public List<HexUnitDataSO> data;
}
