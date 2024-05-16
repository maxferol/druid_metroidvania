using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;

public abstract class StateMachine<EState> : MonoBehaviour where EState : Enum
{
    protected Dictionary<EState, BaseState<EState>> States = new Dictionary<EState, BaseState<EState>>();
    protected BaseState<EState> CurrentState;

    protected EState nextStateKey; 

    void Start()
    {
        CurrentState.EnterState();
    }


    void Update()
    {
        SelfUpdate();
    }

    public abstract void SelfUpdate();
    public abstract void SelfFixedUpdate();

    private void FixedUpdate()
    {
        SelfFixedUpdate();
        nextStateKey = CurrentState.GetNextState();
        if (!nextStateKey.Equals(CurrentState.StateKey))
        {
            TransitionToState(nextStateKey);
        }
        CurrentState.FixedUpdateState();
    }

    public void TransitionToState(EState stateKey)
    {
        CurrentState.ExitState();
        CurrentState = States[stateKey];
        CurrentState.EnterState();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        CurrentState.OnTriggerEnter2D(collision);
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        CurrentState.OnTriggerStay2D(collision);
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        CurrentState.OnTriggerExit2D(collision);
    }
}
