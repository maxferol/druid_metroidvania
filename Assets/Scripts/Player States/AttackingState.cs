using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackingState : PlayerState
{
    private float attackDurationLeft;

    public AttackingState(PlayerContext context, PlayerStateMachine.EPlayerState stateKey) : base(context, stateKey)
    {

    }

    public override void EnterState()
    {
        attackDurationLeft = Context._fightSystem.attackTimer;
        Debug.Log("Entered Attacking State");
    }

    public override void ExitState()
    {
        Debug.Log("Left Attacking State");
    }

    public override void UpdateState()
    {

    }

    public override void FixedUpdateState()
    {
        attackDurationLeft = Context._fightSystem.attackTimer;
    }

    public override PlayerStateMachine.EPlayerState GetNextState()
    {
        if (Context._fightSystem.isAttacking)
            return StateKey;
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
