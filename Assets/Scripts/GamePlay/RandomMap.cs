using System;
using System.Collections;
using System.Collections.Generic;
using Map;
using UnityEngine;

public class RandomMap : MonoBehaviour
{
    [SerializeField] private HexMapGenerator hexMapGenerator;

    private void Start()
    {
        hexMapGenerator.GenerateMap(40, 50, false);
    }
}
