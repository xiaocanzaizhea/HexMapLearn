using System;
using UnityEngine;
using UnityEngine.EventSystems;

namespace Map
{
	public class HexGameUI : MonoBehaviour, IGameEndHandler
	{

		public GameObject view;
		
		public HexGrid Grid
		{
			get
			{
				return GameManager.RunTimeData.grid;
			}
		}

		public void SetEditMode (bool toggle) {
			enabled = !toggle;
			Grid.ShowUI(!toggle);
			Grid.ClearPath();
			if (toggle) {
				Shader.EnableKeyword("HEX_MAP_EDIT_MODE");
			}
			else {
				Shader.DisableKeyword("HEX_MAP_EDIT_MODE");
			}
		}

		private void Awake()
		{
			view.SetActive(true);
		}
		
		public void OnGameEnd(bool isVictory)
		{
			view.SetActive(false);
		}

		#region MyRegion

		void Update () {
			// if (!EventSystem.current.IsPointerOverGameObject()) {
			// 	if (Input.GetMouseButtonDown(0))
			// 	{
			// 		DoLeftMouseDown();
			// 	}
			// 	else if (selectedUnit) {
			// 		if (Input.GetMouseButtonDown(1)) {
			// 			DoMove();
			// 		}
			// 		else {
			// 			DoPathfinding();
			// 		}
			// 	}
			// }
		}

		/*
		 * // 处理鼠标左键点击
		void DoLeftMouseDown()
		{
			var cell = Utils.GetCellUnderCursor();
			if (!cell) return;
			if (cell == currentCell)
			{
				selectedUnit = null;
				currentCell = null;
				return;
			}

			if (cell)
			{
				if (!cell.Unit)
				{
					if (BarCurrentUnit != null && BarCurrentUnit.UnitCount > 0)
					{
						DoPlace(cell);
					}
				}
				else
				{
					DoSelection();
					if(selectedUnit != null) 
						EventManager.Instance.TriggerUnitSelection(selectedUnit, true);
				}
			}
		}

		// 选择单位
		void DoSelection () {
			Grid.ClearPath();
			UpdateCurrentCell();
			if (currentCell) {
				selectedUnit = currentCell.Unit;
			}
		}

		// 放置单位
		void DoPlace(HexCell cell)
		{
			if (BarCurrentUnit == null) return;
			var hexUnit = Instantiate(BarCurrentUnit.unitSo.prefab);
			if(hexUnit != null)
				Grid.AddUnit(hexUnit, cell, Random.Range(0, 360));
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

		// 移动单位
		void DoMove () {
			if (Grid.HasPath) {
				selectedUnit.Travel(Grid.GetPath());
				Grid.ClearPath();
			}
		}

		bool UpdateCurrentCell () {
			HexCell cell =
				Grid.GetCell(Camera.main.ScreenPointToRay(Input.mousePosition));
			if (cell != currentCell) {
				currentCell = cell;
				return true;
			}
			return false;
		}
		 */

		#endregion
	}
}