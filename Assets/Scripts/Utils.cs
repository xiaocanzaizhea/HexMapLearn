using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Utils : MonoBehaviour
{
    public static HexGrid Grid
    {
        get
        {
            return GameManager.RunTimeData.grid;
        }
    }
    
    // 获取鼠标下的位置
    public static HexCell GetCellUnderCursor () {
        return
            Grid.GetCell(Camera.main.ScreenPointToRay(Input.mousePosition));
    }

    // 判断是否可以移动过去， 要求已被探索，并且不是水下，上面没有单位
    public static bool IsValidDestination(HexCell cell)
    {
        return cell.IsExplored && !cell.IsUnderwater && !cell.Unit;
    }
    
    // 初始地图生成
    public static bool InitMapValidDestination(HexCell cell)
    {
        return !cell.IsUnderwater && !cell.Unit;
    }

    // 获取随机位置
    public static HexCell GetValidRandomCellInDistance(HexCell location, int dis = 1)
    {
        if (dis <= 0 || location == null) return null;
        
        int maxRange = dis;
        var hexCoordinates = location.coordinates;

        for (int attempt = 0; attempt < 20; attempt++)
        {
            int x = Random.Range(-maxRange, maxRange + 1);
            int z = Random.Range(-maxRange, maxRange + 1);
        
            HexCoordinates coordinates = new HexCoordinates(
                hexCoordinates.X + x, 
                hexCoordinates.Z + z
            );
        
            HexCell targetLocation = Grid.GetCell(coordinates);
            
            if (targetLocation != null && IsValidDestination(targetLocation))
            {
                return targetLocation;
            }
        }

        return null;
    }

    // 获取两个单位之间的距离
    public static int GetDistance(HexUnit a, HexUnit b)
    {
        return a.Location.coordinates.DistanceTo(b.Location.coordinates);
    }
}
