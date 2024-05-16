using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IdleState : PlayerState
{
    public IdleState(PlayerContext context, PlayerStateMachine.EPlayerState stateKey) : base(context, stateKey)
    {

    }

    public override void EnterState()
    {
        //Debug.Log("Entered Idle State");
    }

    public override void ExitState()
    {
        //Debug.Log("Left Idle State");
    }

    public override void UpdateState()
    {

    }

    public override void FixedUpdateState()
    {

    }

    public override PlayerStateMachine.EPlayerState GetNextState()
    {
        if (Context.dashPressed && Context._dashCooldownLeft <= 0)
            return PlayerStateMachine.EPlayerState.Dashing;
        if (Context.jumpPressed)
            return PlayerStateMachine.EPlayerState.Jumping;
        if (!Context.isOnGround)
        {
            if (Context.hitsWall)
                return PlayerStateMachine.EPlayerState.WallSliding;
            return PlayerStateMachine.EPlayerState.Falling;
        }
        if (Context.runDirection != 0)
            return PlayerStateMachine.EPlayerState.Running;
        return StateKey;
    }

    public override void OnTriggerEnter2D(Collider2D collision)
    {

    }

    public override void OnTriggerStay2D(Collider2D collision)
    {

    }

    public override void OnTriggerExit2D(Collider2D collision)
    {

    }
}