using UnityEngine;

public class PlayerUnit : HexUnit
{
    // 玩家单位单回合只能移动一次
    protected bool hasMoved = false;
    protected FSM fsm;

    protected override void Start()
    {
        base.Start();
        PlayerBlackBoard playerBlackBoard = new PlayerBlackBoard(animator);
        fsm = new FSM(playerBlackBoard);
        fsm.AddState(StateType.Idle, new PlayerAI_IdleState(fsm));
        fsm.AddState(StateType.Move, new PlayerAI_MoveState(fsm));
        fsm.SwitchState(StateType.Idle);
    }

    protected override void Update()
    {
        base.Update();
        fsm.OnUpdate();
    }

    protected override bool IsPlayerUnit()
    {
        return true;
    }

    protected override void OnNextRound()
    {
        hasMoved = false;
    }

    public override void Die()
    {
        base.Die();
        if (Location)
        {
            Grid.DecreaseVisibility(Location, VisionRange);
        }
    }
}

public class PlayerBlackBoard : Blackboard
{
    public PlayerBlackBoard(Animator animator)
    {
        this.animator = animator;
    }
    
    public Animator animator;
}

public class PlayerAI_IdleState : IState
{
    private FSM fsm;
    private PlayerBlackBoard playerBlackBoard;

    public PlayerAI_IdleState(FSM fsm)
    {
        this.fsm = fsm;
        this.playerBlackBoard = fsm.blackboard as PlayerBlackBoard;
    }
    
    public void OnEnter() { }

    public void OnExit() { }

    public void OnUpdate()
    {
        
    }
}

public class PlayerAI_MoveState : IState
{
    private FSM fsm;
    private PlayerBlackBoard playerBlackBoard;

    public PlayerAI_MoveState(FSM fsm)
    {
        this.fsm = fsm;
        this.playerBlackBoard = fsm.blackboard as PlayerBlackBoard;
    }
    
    public void OnEnter()
    {
        playerBlackBoard.animator.SetBool(Settings.IsMoving, true);
    }

    public void OnExit()
    {
        playerBlackBoard.animator.SetBool(Settings.IsMoving, false);
    }

    public void OnUpdate()
    {
        
    }
}