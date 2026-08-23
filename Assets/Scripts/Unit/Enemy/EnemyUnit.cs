using System;
using System.Collections.Generic;
using UnityEngine;

public class EnemyUnit : HexUnit
{
    protected FSM fsm;
    
    protected override bool IsPlayerUnit()
    {
        return false;
    }

    protected override void OnNextRound()
    {
        CheckState();
        fsm.OnNextRound();
    }

    protected override void OnClicked()
    {
        
    }

    protected override void Start()
    {
        base.Start();
        EnemyBlackBoard enemyBlackBoard = new EnemyBlackBoard(this);
        fsm = new FSM(enemyBlackBoard);
        fsm.AddState(StateType.Idle, new EnemyAI_IdleState(fsm));
        fsm.AddState(StateType.Move, new EnemyAI_MoveState(fsm));
        fsm.AddState(StateType.Attack, new EnemyAI_AttackState(fsm));
        fsm.SwitchState(StateType.Idle);
    }

    void CheckState()
    {
        // 检查判断下一步应该变什么状态
        // 先判断距离范围内有人
        List<HexUnit> playerUnits = Grid.playerUnits;
        int nearest = Int32.MaxValue;
        HexUnit target = null;
        foreach (HexUnit playerUnit in playerUnits)
        {
            int d = Utils.GetDistance(playerUnit, this);
            if (d < nearest && d <= dataSo.viewRange)
            {
                nearest = d;
                target = playerUnit;
            }
        }

        // 判断在视野范围内还是攻击范围
        if (target != null)
        {
            Debug.Log(dataSo.id + "视野范围内发现" + target.dataSo.id);
            (fsm.blackboard as EnemyBlackBoard).target = target;
            if (Utils.GetDistance(target, this) <= dataSo.attackRange)
            {
                fsm.SwitchState(StateType.Attack);
            }
            else
            {
                fsm.SwitchState(StateType.Move);
            }
        }
        else
        {
            Debug.Log(dataSo.id + "视野范围内没有人");
            (fsm.blackboard as EnemyBlackBoard).target = null;
            fsm.SwitchState(StateType.Move);
        }
    }
}

public class EnemyBlackBoard : Blackboard
{
    public HexUnit unit;
    public HexUnit target;

    public EnemyBlackBoard(HexUnit unit)
    {
        this.unit = unit;
    }
}

public class EnemyAI_IdleState : IState
{
    private FSM fsm;
    private EnemyBlackBoard enemyBlackBoard;

    public EnemyAI_IdleState(FSM fsm)
    {
        this.fsm = fsm;
        this.enemyBlackBoard = fsm.blackboard as EnemyBlackBoard;
    }
    
    public void OnEnter() { }

    public void OnExit() { }

    public void OnNextRound() { }
}

public class EnemyAI_MoveState : IState
{
    private FSM fsm;
    private EnemyBlackBoard enemyBlackBoard;

    public EnemyAI_MoveState(FSM fsm)
    {
        this.fsm = fsm;
        this.enemyBlackBoard = fsm.blackboard as EnemyBlackBoard;
    }
    
    public void OnEnter()
    {
        
    }

    public void OnExit()
    {
        
    }

    public void OnNextRound()
    {
        HexUnit unit = enemyBlackBoard.unit;
        HexGrid Grid = unit.Grid;
        HexCell targetLocation = null;
        if (enemyBlackBoard.target != null)
        {
            Debug.Log(unit.dataSo.id + "跟随敌人");
            targetLocation = Utils.GetValidRandomCellInDistance(enemyBlackBoard.target.Location);
        }
        else
        {
            Debug.Log(unit.dataSo.id + "随机游走");
            targetLocation = Utils.GetValidRandomCellInDistance(unit.Location, unit.dataSo.moveRange);
        }
        unit.Grid.FindPath(unit.Location, targetLocation, unit, false);
        if (Grid.HasPath)
        {
            unit.Travel(Grid.GetPath());
            Grid.ClearPath();
        }
    }
}

public class EnemyAI_AttackState : IState
{
    private FSM fsm;
    private EnemyBlackBoard enemyBlackBoard;

    public EnemyAI_AttackState(FSM fsm)
    {
        this.fsm = fsm;
        this.enemyBlackBoard = fsm.blackboard as EnemyBlackBoard;
    }
    
    public void OnEnter()
    {
        
    }

    public void OnExit()
    {
        
    }

    public void OnNextRound()
    {
        HexUnit unit = enemyBlackBoard.unit;
        HexUnit target = enemyBlackBoard.target;
        int attackPower = enemyBlackBoard.unit.dataSo.attack;
        if (target != null)
        {
            target.TakeDamage(unit, attackPower);
            Debug.Log(target.dataSo.id + "受到伤害" + attackPower);
        }
    }
}