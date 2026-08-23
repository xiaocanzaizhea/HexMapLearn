using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class TestBlackBoard : Blackboard
{
    public float idleTime;
}

public class AI_IdleState : IState
{
    private float idleTimer;
    private FSM fsm;
    private TestBlackBoard blackboard;
    public AI_IdleState(FSM fsm)
    {
        this.fsm = fsm;
        this.blackboard = fsm.blackboard as TestBlackBoard;
    }
    public void OnEnter()
    {
        idleTimer = 0;
    }

    public void OnExit()
    {
    }

    public void OnNextRound()
    {
        idleTimer += Time.deltaTime;
        if (idleTimer > blackboard.idleTime)
        {
            this.fsm.SwitchState(StateType.Move);
        }
    }
}

public class AI_MoveState : IState
{
    private FSM fsm;
    private TestBlackBoard blackboard;
    private Vector2 targetPos;
    public AI_MoveState(FSM fsm)
    {
        this.fsm = fsm;
        this.blackboard = fsm.blackboard as TestBlackBoard;
    }
    public void OnEnter()
    {
        
    }

    public void OnExit()
    {
    }

    public void OnNextRound()
    {
    }
}

public class Ai : MonoBehaviour
{
    private FSM fsm;
    public TestBlackBoard blackBoard;
    // Start is called before the first frame update
    void Start()
    {
        fsm = new FSM(blackBoard);
        fsm.AddState(StateType.Idle, new AI_IdleState(fsm));
        fsm.AddState(StateType.Move, new AI_MoveState(fsm));
        fsm.SwitchState(StateType.Idle);
    }

    // Update is called once per frame
    void Update()
    {
        fsm.OnNextRound();
    }
}
