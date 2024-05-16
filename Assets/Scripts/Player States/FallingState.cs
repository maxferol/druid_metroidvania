using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FallingState : PlayerState
{
    public FallingState(PlayerContext context, PlayerStateMachine.EPlayerState stateKey) : base(context, stateKey)
    {

    }

    public override void EnterState()
    {
        //Debug.Log("Entered Falling State");
    }

    public override void ExitState()
    {
        //Debug.Log("Left Falling State");
    }

    public override void UpdateState()
    {

    }

    public override void FixedUpdateState()
    {
        var newPos = Context._plRB.position + new Vector2(Context.runDirection * Context._runSpeed, Context._fallingSpeed);
        if (!float.IsNaN(Context.groundLevel) && (newPos.y < Context.groundLevel))
        {
            newPos.y = Context.groundLevel;
        }
        Context._plRB.MovePosition(newPos);
        if (Context.runDirection != 0)
            Context._plRB.transform.localScale = new Vector3(Context.runDirection, 1, 1);
    }

    public override PlayerStateMachine.EPlayerState GetNextState()
    {
        if (Context.dashPressed && Context._dashCooldownLeft <= 0)
            return PlayerStateMachine.EPlayerState.Dashing;
        if (Context.isOnGround)
            return PlayerStateMachine.EPlayerState.Idle;
        if (Context.hitsWall)
        {
            if ((Context.isWallRightSide && Context.runDirection > 0) || (!Context.isWallRightSide && Context.runDirection < 0))
                return PlayerStateMachine.EPlayerState.WallSliding;
        }
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
