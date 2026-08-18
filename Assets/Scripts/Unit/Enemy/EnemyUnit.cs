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
        
    }

    protected override void Start()
    {
        base.Start();
        EnemyBlackBoard enemyBlackBoard = new EnemyBlackBoard(animator);
        fsm = new FSM(enemyBlackBoard);
        fsm.AddState(StateType.Idle, new EnemyAI_IdleState(fsm));
        fsm.AddState(StateType.Move, new EnemyAI_MoveState(fsm));
        fsm.SwitchState(StateType.Idle);
    }

    protected override void Update()
    {
        base.Update();
        fsm.OnUpdate();
        UpdateEnemyAI();
    }

    void UpdateEnemyAI()
    {
        
    }
}

public class EnemyBlackBoard : Blackboard
{
    public Animator animator;

    public EnemyBlackBoard(Animator animator)
    {
        this.animator = animator;
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

    public void OnUpdate()
    {
        
    }
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

    public void OnUpdate()
    {
        
    }
}