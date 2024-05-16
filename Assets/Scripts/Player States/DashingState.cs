using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DashingState : PlayerState
{
    private float dashDurationLeft;

    public DashingState(PlayerContext context, PlayerStateMachine.EPlayerState stateKey) : base(context, stateKey)
    {

    }

    public override void EnterState()
    {
        dashDurationLeft = Context._dashDuration;
        //Debug.Log("Entered Dashing State");
    }

    public override void ExitState()
    {
        Context._dashCooldownLeft = Context._dashCooldown;
        Context.dashPressed = false;
        //Debug.Log("Left Dashing State");
    }

    public override void UpdateState()
    {

    }

    public override void FixedUpdateState()
    {
        Context._plRB.MovePosition(Context._plRB.position + new Vector2(Context._plRB.transform.localScale.x * Context._dashSpeed, 0));
        dashDurationLeft -= Time.fixedDeltaTime;
    }

    public override PlayerStateMachine.EPlayerState GetNextState()
    {
        if (dashDurationLeft <= 0)
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
