using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RunningState : PlayerState
{
    public bool isFacedRight;

    public RunningState(PlayerContext context, PlayerStateMachine.EPlayerState stateKey) : base(context, stateKey)
    {

    }

    public override void EnterState()
    {
        //Debug.Log("Entered Running State");
        Context._jumpsLeft = Context._jumpsMaxNumber;
    }

    public override void ExitState()
    {
        //Debug.Log("Left Running State");
    }

    public override void UpdateState()
    {

    }

    public override void FixedUpdateState()
    {
        if (Context.runDirection != 0)
        {
            Context._plRB.MovePosition(Context._plRB.position + Context.runDirection * Context._runSpeed * Vector2.right);
            Context._plRB.transform.localScale = new Vector3(Context.runDirection, 1, 1);
        }
    }

    public override PlayerStateMachine.EPlayerState GetNextState()
    {
        if (Context._fightSystem.isAttacking)
            return PlayerStateMachine.EPlayerState.Attacking;
        if (Context.dashPressed && Context._dashCooldownLeft <= 0)
            return PlayerStateMachine.EPlayerState.Dashing;
        if (Context.isOnGround && Context.jumpPressed)
            return PlayerStateMachine.EPlayerState.Jumping;
        if (!Context.isOnGround)
            return PlayerStateMachine.EPlayerState.Falling;
        if (Context.runDirection == 0)
            return PlayerStateMachine.EPlayerState.Idle;
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
