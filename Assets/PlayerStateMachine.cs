using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerStateMachine : StateMachine<PlayerStateMachine.EPlayerState>
{
    public enum EPlayerState
    {
        Idle,
        Running,
        Dashing,
        Jumping,
        Falling,
    }

    private PlayerContext context;

    [SerializeField] private bool jumpPressed;
    [SerializeField] private bool dashPressed;
    [SerializeField] private float runDirection;

    private void Awake()
    {
        context = new PlayerContext(dashPressed, jumpPressed, runDirection);
    }

    private void InitializeStates()
    {
        States.Add(EPlayerState.Idle, new IdleState(context, EPlayerState.Idle));
        States.Add(EPlayerState.Running, new RunningState(context, EPlayerState.Running));
        States.Add(EPlayerState.Dashing, new DashingState(context, EPlayerState.Dashing));
        States.Add(EPlayerState.Jumping, new JumpingState(context, EPlayerState.Jumping));
        States.Add(EPlayerState.Falling, new FallingState(context, EPlayerState.Falling));
    }
}
