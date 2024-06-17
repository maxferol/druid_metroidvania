using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JumpingState : PlayerState
{
    private float jumpTimeLeft;
    private float jumpingFromWallTime = 0;

    public Vector3 newPos;

    public JumpingState(PlayerContext context, PlayerStateMachine.EPlayerState stateKey) : base(context, stateKey)
    {

    }

    public override void EnterState()
    {
        jumpingFromWallTime = 0;
        jumpTimeLeft = Context._jumpMaxDuration;
        Context.jumpPressed = false;
        //Debug.Log("Entered Jumping State");
    }

    public override void ExitState()
    {
        Context.jumpingFromWall = false;
        //Debug.Log("Left Jumping State");
    }

    public override void UpdateState()
    {

    }

    public override void FixedUpdateState()
    {
        if (Context.jumpingFromWall && (jumpingFromWallTime < Context._jumpingFromWallMinTime))
        {
            newPos = Context._plRB.position + new Vector2(Context._plRB.transform.localScale.x, 1) * Context._jumpingFromWallSpeed;
            jumpingFromWallTime += Time.fixedDeltaTime;
        }
        else
        {
            newPos = Context._plRB.position + new Vector2(Context.runDirection * Context._runSpeed, Context._jumpSpeed);
            if (Context.runDirection != 0)
                Context._plRB.transform.localScale = new Vector3(Context.runDirection, 1, 1);
        }
        Context._plRB.MovePosition(newPos);
        jumpTimeLeft -= Time.fixedDeltaTime;
    }

    public override PlayerStateMachine.EPlayerState GetNextState()
    {
        if (Context.dashPressed && Context._dashCooldownLeft <= 0)
            return PlayerStateMachine.EPlayerState.Dashing;
        if ((jumpTimeLeft > 0) && (Context.jumpIsHeld))
        {
            return StateKey;
        }
        if (Context.isOnGround)
        {
            if (Context.runDirection != 0)
                return PlayerStateMachine.EPlayerState.Running;
            return PlayerStateMachine.EPlayerState.Idle;
        }
        if (Context.hitsWall)
        {
            if ((Context.isWallRightSide && Context.runDirection > 0) || (!Context.isWallRightSide && Context.runDirection < 0))
                return PlayerStateMachine.EPlayerState.WallSliding;
        }
        return PlayerStateMachine.EPlayerState.Falling;
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
