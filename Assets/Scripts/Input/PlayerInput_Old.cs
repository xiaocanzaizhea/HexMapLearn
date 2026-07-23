using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using Random = UnityEngine.Random;

public class PlayerInput_Old : MonoSingleton<PlayerInput_Old>
{
    // public GameObject hexMapEditor;
    // public GameObject gameUI;
    //
    // private HexGrid Grid => GameManager.RunTimeData.grid;
    // private UnitsBarItem BarCurrentUnit => UnitBuildablePanel.Instance.currentSelectedUnit;
    //
    // private HexCell currentCell;
    // private HexUnit selectedUnit;
    //
    // private void Start()
    // {
    //     hexMapEditor.gameObject.SetActive(false);
    //     gameUI.gameObject.SetActive(true);
    // }
    //
    // private void Update()
    // {
    //     if (Input.GetKeyDown(KeyCode.F1)) hexMapEditor.SetActive(!hexMapEditor.activeSelf);
    //     if (Input.GetKeyDown(KeyCode.F2)) gameUI.SetActive(!gameUI.activeSelf);
    //     
    //     if (!EventSystem.current.IsPointerOverGameObject())
    //     {
    //         if (Input.GetMouseButtonDown(0))
    //         {
    //             DoLeftMouseDown();
    //         }else if (selectedUnit)
    //         {
    //             if (Input.GetMouseButtonDown(1))
    //             {
    //                 DoMove();
    //             }
    //             else
    //             {
    //                 DoPathfinding();
    //             }
    //         }
    //     }
    // }
    //
    // void DoLeftMouseDown()
    // {
    //     HexCell cell = Utils.GetCellUnderCursor();
    //     if (cell == null)
    //     {
    //         EventManager.Instance.TriggerUnitUnSelection();
    //         return;
    //     }
    //
    //     if (cell.Unit == null)
    //     {
    //         if (BarCurrentUnit == null)
    //         {
    //             EventManager.Instance.TriggerUnitUnSelection();
    //             return;
    //         }
    //         else
    //         {
    //             // 放置单位
    //             DoPlace(cell);
    //             return;
    //         }
    //     }
    //     else // 该cell有单位
    //     {
    //         if (cell.Unit == selectedUnit)
    //         {
    //             selectedUnit = null;
    //         }
    //         else
    //         {
    //             DoSelection();
    //         }
    //     }
    // }
    //
    // void DoPlace(HexCell cell)
    // {
    //     if (BarCurrentUnit == null || BarCurrentUnit.UnitCount <= 0) return;
    //     var hexUnit = Instantiate(BarCurrentUnit.unitSo.prefab);
    //     hexUnit.unitSo = BarCurrentUnit.unitSo;
    //     if(hexUnit != null)
    //         Grid.AddUnit(hexUnit, cell, Random.Range(0, 360));
    // }
    //
    // void DoSelection()
    // {
    //     Grid.ClearPath();
    //     UpdateCurrentCell();
    //     if (currentCell && currentCell.Unit.unitSo.campType == CampType.Human)
    //     {
    //         selectedUnit = currentCell.Unit;
    //         EventManager.Instance.TriggerUnitSelection(selectedUnit, true);
    //     }
    // }
    //
    // void DoPathfinding () {
    //     if (UpdateCurrentCell()) {
    //         // 细胞存在且可以到达
    //         if (currentCell && Utils.IsValidDestination(currentCell)) {
    //             Grid.FindPath(selectedUnit.Location, currentCell, selectedUnit);
    //         }
    //         else {
    //             Grid.ClearPath();
    //         }
    //     }
    // }
    //
    // bool UpdateCurrentCell()
    // {
    //     HexCell cell = Utils.GetCellUnderCursor();
    //     if (cell != currentCell)
    //     {
    //         currentCell = cell;
    //         return true;
    //     }
    //     return false;
    // }
    //
    // void DoMove () {
    //     if (Grid.HasPath) {
    //         selectedUnit.Travel(Grid.GetPath());
    //         Grid.ClearPath();
    //     }
    // }
}
