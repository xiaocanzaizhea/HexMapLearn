using System;
using UnityEngine;
using UnityEngine.EventSystems;

namespace Map
{
	public class HexGameUI : MonoBehaviour
	{
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
	}
}