using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum StateType
{
    Idle,
    Move,
    Find_Target,
    Attack,
    Die,
    Success,
}

public interface IState
{
    void OnEnter();
    void OnExit();
    void OnUpdate();
}

[Serializable]
public class Blackboard
{
    
}

public class FSM
{
    public IState currentState;
    public Dictionary<StateType, IState> states;
    public Blackboard blackboard;
    
    public FSM(Blackboard blackboard)
    {
        this.states = new Dictionary<StateType, IState>();
        this.blackboard = blackboard;
    }

    public void AddState(StateType stateType, IState state)
    {
        if (states.ContainsKey(stateType))
        {
            Debug.Log("[AddState] State already added" + stateType);
            return;
        }
        states.Add(stateType, state);
    }

    public void SwitchState(StateType stateType)
    {
        if (!states.ContainsKey(stateType))
        {
            Debug.Log("[SwitchState] State not added");
            return;
        }

        if (currentState != null)
        {
            currentState.OnExit();
        }
        
        currentState = states[stateType];
        currentState.OnEnter();
    }

    public void OnUpdate()
    {
        currentState.OnUpdate();
    }
}
