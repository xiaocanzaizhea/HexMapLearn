using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

[RequireComponent(typeof(HexGrid))]
public class InitialHexMap : MonoBehaviour
{
    private HexGrid hexGrid;
    
    private void Start()
    {
        hexGrid = GetComponent<HexGrid>();
        StartCoroutine(SetupUnit());
    }

    IEnumerator SetupUnit()
    {
        yield return new WaitForSeconds(0.5f);
        GameManager.Event.Broadcast(HexEvents.GameStart.ToString(), GameEventParameter.Empty);
        SetupBaseCamp();
        SetupEnemies();
    }

    void SetupBaseCamp()
    {
        HexCell randomCell = GetRandomCell();
        if (randomCell)
        {
            HexUnitDataSO baseCamp = GameManager.Instance.PlayerDataDic["BaseCamp"];
            var baseCampPrefab = Instantiate(baseCamp.prefab);
            hexGrid.AddUnit(baseCampPrefab, randomCell, Random.Range(0, 360), false);
            Debug.Log("基础营地生成成功，位置在" + randomCell.coordinates.ToString());
        }
        else
        {
            Debug.Log("初始营地未生成成功");
        }
    }

    void SetupEnemies()
    {
        for (int i = 0; i < GameManager.RunTimeData.enemyUnitStartCount; i++)
        {
            HexCell randomCell = GetRandomCell();
            if (randomCell)
            {
                var unit = Instantiate(GameManager.Instance.EnemyDataDic["Goblin"].prefab);
                hexGrid.AddUnit(unit, randomCell, Random.Range(0, 360), false);
                Debug.Log("敌人生成成功");
            }
            else
            {
                Debug.Log("敌人未生成成功");
            }
        }
    }

    private HexCell GetRandomCell()
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
