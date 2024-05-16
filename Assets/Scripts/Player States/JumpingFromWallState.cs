using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JumpingFromWallState : PlayerState
{
    private float jumpTimeLeft;

    public JumpingFromWallState(PlayerContext context, PlayerStateMachine.EPlayerState stateKey) : base(context, stateKey)
    {

    }

    public override void EnterState()
    {
        jumpTimeLeft = Context._jumpMaxDuration;
        Context.jumpPressed = false;
        //Debug.Log("Entered Jumping State");
    }

    public override void ExitState()
    {
        //Debug.Log("Left Jumping State");
    }

    public override void UpdateState()
    {

    }

    public override void FixedUpdateState()
    {
        Context._plRB.MovePosition(Context._plRB.position + new Vector2(Context.runDirection * Context._runSpeed, Context._jumpSpeed));
        if (Context.runDirection != 0)
            Context._plRB.transform.localScale = new Vector3(Context.runDirection, 1, 1);
        jumpTimeLeft -= Time.fixedDeltaTime;
    }

    public override PlayerStateMachine.EPlayerState GetNextState()
    {
        if (Context.dashPressed && Context._dashCooldownLeft <= 0)
            return PlayerStateMachine.EPlayerState.Dashing;
        if ((jumpTimeLeft > 0) && (Context.jumpIsHeld))
            return StateKey;
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
