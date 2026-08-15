using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using Random = UnityEngine.Random;

public class PlayerInput_Old : MonoSingleton<PlayerInput_Old>
{
    private HexGrid Grid => GameManager.RunTimeData.grid;
    private UnitsBuildableItem CurrentUnitInUI => GameManager.RunTimeData.CurrentSelectedUnitInUI;
    
    private HexCell currentCell;
    private HexUnit selectedUnit;
    
    private void Update()
    {
        if (!EventSystem.current.IsPointerOverGameObject())
        {
            if (Input.GetMouseButtonDown(0))
            {
                DoLeftMouseDown();
            }else if (selectedUnit)
            {
                if (Input.GetMouseButtonDown(1))
                {
                    DoMove();
                }
                else
                {
                    DoPathfinding();
                }
            }
        }
    }
    
    void DoLeftMouseDown()
    {
        HexCell cell = Utils.GetCellUnderCursor();
        if (cell == null)
        {
            // EventManager.Instance.TriggerUnitUnSelection();
            return;
        }
    
        if (cell.Unit == null)
        {
            if (CurrentUnitInUI == null)
            {
                // EventManager.Instance.TriggerUnitUnSelection();
                return;
            }
            else
            {
                // 放置单位
                DoPlace(cell);
                return;
            }
        }
        else // 该cell有单位
        {
            if (cell.Unit == selectedUnit)
            {
                selectedUnit = null;
            }
            else
            {
                DoSelection();
            }
        }
    }
    
    void DoPlace(HexCell cell)
    {
        if (CurrentUnitInUI == null || CurrentUnitInUI.UnitCount <= 0) return;
        var hexUnit = Instantiate(CurrentUnitInUI.dataSo.prefab);
        hexUnit.dataSo = CurrentUnitInUI.dataSo;
        if(hexUnit != null)
            Grid.AddUnit(hexUnit, cell, Random.Range(0, 360));
    }
    
    void DoSelection()
    {
        Grid.ClearPath();
        UpdateCurrentCell();
        if (currentCell && currentCell.Unit.dataSo.team == UnitTeam.Player)
        {
            selectedUnit = currentCell.Unit;
            // EventManager.Instance.TriggerUnitSelection(selectedUnit, true);
        }
    }
    
    void DoPathfinding () {
        if (UpdateCurrentCell()) {
            // 细胞存在且可以到达
            if (currentCell && Utils.IsValidDestination(currentCell)) {
                Grid.FindPath(selectedUnit.Location, currentCell, selectedUnit);
            }
            else {
                Grid.ClearPath();
            }
        }
    }
    
    bool UpdateCurrentCell()
    {
        HexCell cell = Utils.GetCellUnderCursor();
        if (cell != currentCell)
        {
            currentCell = cell;
            return true;
        }
        return false;
    }
    
    void DoMove () {
        if (Grid.HasPath) {
            selectedUnit.Travel(Grid.GetPath());
            Grid.ClearPath();
        }
    }
}
