using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;

public abstract class StateMachine<EState> : MonoBehaviour where EState : Enum
{
    protected Dictionary<EState, BaseState<EState>> States = new Dictionary<EState, BaseState<EState>>();
    protected BaseState<EState> CurrentState;

    protected bool IsTransitioningState = false;
    protected EState nextStateKey; 

    void Start()
    {
        CurrentState.EnterState();
    }


    void Update()
    {
        nextStateKey = CurrentState.GetNextState();

        if (!IsTransitioningState)
        {
            if (nextStateKey.Equals(CurrentState.StateKey))
            {
                CurrentState.UpdateState();
            }

            else
                TransitionToState(nextStateKey);
        }
    }

    private void FixedUpdate()
    {
        if (!IsTransitioningState)
        {
            if (nextStateKey.Equals(CurrentState.StateKey))
            {
                CurrentState.UpdateState();
            }

            else
                TransitionToState(nextStateKey);
        }
    }

    public void TransitionToState(EState stateKey)
    {
        IsTransitioningState = true;
        CurrentState.ExitState();
        CurrentState = States[stateKey];
        CurrentState.EnterState();
        IsTransitioningState = false;
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
