using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using Cysharp.Threading.Tasks;
using Unity.VisualScripting;
using UnityEngine;

public class PlayerUnit : HexUnit
{
    public PlayerUnitDataEntity data;
    
    public HexCell Location
    {
        get => location;
        set {
            if (location) {
                Grid.DecreaseVisibility(location, VisionRange);
                location.Unit = null;
            }
            location = value;
            value.Unit = this;
            transform.localPosition = value.Position;
            Grid.MakeChildOfColumn(transform, value.ColumnIndex);
        }
    }

    /*#region MyRegion

    void checkCellResource()
    {
        if(Location == null || Location.FarmLevel == 0) return;
        
        // 提取资源
        EventManager.Instance.TriggerResourceGet(Location.ResourcePoint);
    }

    void TryToGetTargetUnit()
    {
        if (Location)
        {
            EnemyUnit target = Grid.FindEnemyInVisionRange(this);
            if (target != null)
            {
                this.target = target;
            }
        }
    }

    void AttackTargetUnit()
    {
        if(target == null) return;
        
        var distanceTo = this.Location.coordinates.DistanceTo(target.Location.coordinates);
        if (distanceTo <= data.moveRange)
        {
            target.TakeDamage(this, data.attack);
            Debug.Log("玩家单位攻击了敌人");
        }
        else
        {
            target = null;
        }
    }

    #endregion
    */

    protected override void Awake()
    {
        base.Awake();
        base.data = this.data;
    }

    protected override int GetAttackPower()
    {
        return data.attack;
    }

    protected override int GetRetreat()
    {
        return data.retreatTimeRequired;
    }

    protected override int GetVisionRange()
    {
        return data.sightRange;
    }

    public override void Save (BinaryWriter writer) {
        location.coordinates.Save(writer);
        writer.Write(orientation);
        writer.Write(GameManager.Instance.PlayerUnitsData.Values.ToList().FindIndex(e => e.id == data.id));
    }
    
    public override void Load(BinaryReader reader, HexGrid grid)
    {
        HexCoordinates coordinates = HexCoordinates.Load(reader);
        float orientation = reader.ReadSingle();
        int index = reader.ReadInt32();
        PlayerUnitDataEntity entity = GameManager.Instance.PlayerUnitsData.Values.ToList()[index];
        grid.AddUnit(
            Instantiate(entity.gameObject.GetComponent<PlayerUnit>()), 
            grid.GetCell(coordinates), 
            orientation
        );
    }
}
