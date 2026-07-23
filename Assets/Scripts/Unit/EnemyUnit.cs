using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using Random = UnityEngine.Random;

public class EnemyUnit : HexUnit<EnemyDataSO>
{
    public new EnemyDataSO data;
    
    protected override void Awake()
    {
        base.Awake();
    }

    protected override string GetID()
    {
        throw new NotImplementedException();
    }

    protected override int GetHealth()
    {
        throw new NotImplementedException();
    }

    protected override int GetDefense()
    {
        throw new NotImplementedException();
    }

    protected override int GetAttackPower() => data.attack;

    protected override int GetRetreat() => -1;

    protected override int GetVisionRange() => data.sightRange;

    public override void Save(BinaryWriter writer)
    {
        throw new NotImplementedException();
    }

    public override void Load(BinaryReader reader, HexGrid grid)
    {
        throw new NotImplementedException();
    }

    #region MyRegion

    // public override void OnRoundStart()
    // {
    //     base.OnRoundStart();
    //
    //     CheckResourcePoint();
    // }
    //
    // public override void OnRoundEnd()
    // {
    //     base.OnRoundEnd();
    //     
    //     // 敌人将在每回合结束时进行操作, 默认是四处移动, 如果在视野范围内看到敌对玩家单位，则朝着单位移动
    //     PlayerUnit target = Grid.FindPlayerInVisionRange(this);
    //     if (target != null)
    //     {
    //         // 尝试走过去攻击
    //         int attackRange = playerUnitDataEntity.attackArgs.attackRange;
    //         int distance = Location.coordinates.DistanceTo(target.Location.coordinates);
    //
    //         if (distance <= attackRange)
    //         {
    //             Debug.Log("在范围内，尝试攻击");
    //             target.TakeDamage(this, playerUnitDataEntity.attackArgs.attackPower);
    //         }
    //         else // 尝试移动过去攻击,走到极限距离
    //         {
    //             Debug.Log("发现目标，尝试移动过去攻击");
    //             MoveToTargetUnit(target);
    //         }
    //     }
    //     else
    //     {
    //         // 漫游
    //         int moveRange = Random.Range(0, playerUnitDataEntity.moveRange);
    //         if (moveRange != 0)
    //         {
    //             RandomRoaming(moveRange);
    //         }
    //     }
    // }

    #endregion

    void CheckResourcePoint()
    {
        if (Location.FarmLevel > 0 && !Location.IsPlundered)
        {
            Location.FarmLevel -= 1;
            Location.IsPlundered = true;
        }
    }
    
    // 游走
    private void RandomRoaming(int moveRange)
    {
        var randomCell = Utils.GetRandomCell(Location, moveRange);

        if (randomCell != null)
        {
            Location = randomCell;
        }
    }

    private void MoveToTargetUnit(PlayerUnit playerUnit)
    {
        var randomCell = Utils.GetRandomCell(playerUnit.Location);

        Grid.FindPath(this.Location, randomCell, this);

        if (Grid.HasPath)
        {
            Travel(Grid.GetPath());
            Grid.ClearPath();
        }
    }
}
