using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

[RequireComponent(typeof(HexGrid))]
public class UnitGenerate : MonoBehaviour
{
    public PlayerUnitDataEntity baseCamp;
    private HexGrid hexGrid;
    
    private void Start()
    {
        baseCamp = GameManager.Instance.PlayerUnitsData["BaseCamp"];
        hexGrid = GetComponent<HexGrid>();
        StartCoroutine(SetupUnit());
    }

    IEnumerator SetupUnit()
    {
        yield return new WaitForSeconds(0.5f);
        SetupBaseCamp();
        SetupResourcePoint();
        SetupEnemies();
    }

    void SetupBaseCamp()
    {
        HexCell randomCell = GetRandomCell();
        if (randomCell)
        {
            var baseCampPrefab = Instantiate(GameManager.AssetLoader
                .LoadAsset<PlayerUnitDataEntity>("BaseCamp"));
            hexGrid.AddUnit(baseCampPrefab, randomCell, Random.Range(0, 360), false);
            Debug.Log("基础营地生成成功，位置在" + randomCell.coordinates.ToString());
        }
        else
        {
            Debug.Log("初始营地未生成成功");
        }
    }

    void SetupResourcePoint()
    {
        for (int i = 0; i < 10; i++)
        {
            HexCell randomCell = GetRandomCell();
            if (randomCell)
            {
                randomCell.FarmLevel = Random.Range(1, 4);
            }
        }
    }

    void SetupEnemies()
    {
        // for (int i = 0; i < 1; i++)
        // {
        //     HexCell randomCell = GetRandomCell();
        //     if (randomCell)
        //     {
        //         var unit = Instantiate(GameManager.RunTimeData.enemyUnits[0].prefab);
        //         hexGrid.AddUnit(unit, randomCell, Random.Range(0, 360), false);
        //     }
        //     else
        //     {
        //         Debug.Log("敌人未生成成功");
        //     }
        // }
    }

    public HexCell GetRandomCell()
    {
        HexCell hexCell = null;
        for (int i = 0; i < 100; i++)
        {
            int randomX = Random.Range(0, hexGrid.cellCountX);
            int randomZ = Random.Range(0, hexGrid.cellCountZ);
    
            hexCell = hexGrid.GetCell(randomX, randomZ);
            if (Utils.InitMapValidDestination(hexCell))
            {
                return hexCell;
            }
        }
        
        return null;
    }
}
