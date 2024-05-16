using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WallSlidingState : PlayerState
{

    public WallSlidingState(PlayerContext context, PlayerStateMachine.EPlayerState stateKey) : base(context, stateKey)
    {

    }

    public override void EnterState()
    {
        if (Context.isWallRightSide)
        {
            Context._plRB.transform.localScale = new Vector3(-1, Context._plRB.transform.localScale.y, Context._plRB.transform.localScale.z);
        }
        else
        {
            Context._plRB.transform.localScale = new Vector3(1, Context._plRB.transform.localScale.y, Context._plRB.transform.localScale.z);
        }
        Debug.Log("Entered WallSliding State");
    }

    public override void ExitState()
    {
        Debug.Log("Left WallSliding State");
    }

    public override void UpdateState()
    {

    }

    public override void FixedUpdateState()
    {
        var newPos = Context._plRB.position + new Vector2(0, Context._wallSlidingSpeed);
        if (!float.IsNaN(Context.groundLevel) && (newPos.y < Context.groundLevel))
        {
            newPos.y = Context.groundLevel;
        }
        Context._plRB.MovePosition(newPos);
        //if (Context.runDirection != 0)
            //Context._plRB.transform.localScale = new Vector3(Context.runDirection, 1, 1);
    }

    public override PlayerStateMachine.EPlayerState GetNextState()
    {
        if (Context.dashPressed && Context._dashCooldownLeft <= 0)
            return PlayerStateMachine.EPlayerState.Dashing;
        if (Context.jumpPressed)
        {
            Context.jumpingFromWall = true;
            return PlayerStateMachine.EPlayerState.Jumping;
        }
        if (!Context.isOnGround)
        {
            if (((Context.isWallRightSide) && (Context.runDirection < 0)) || ((!Context.isWallRightSide) && (Context.runDirection > 0)))
            {
                return PlayerStateMachine.EPlayerState.Falling;
            }
            if (!Context.hitsWall)
                return PlayerStateMachine.EPlayerState.Falling;
            return StateKey;
        }
        return PlayerStateMachine.EPlayerState.Idle;
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
