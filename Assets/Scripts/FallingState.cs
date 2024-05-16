using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FallingState : PlayerState
{
    public FallingState(PlayerContext context, PlayerStateMachine.EPlayerState stateKey) : base(context, stateKey)
    {
        Context = context;
    }

    public override void EnterState()
    {

    }
    public override void ExitState()
    {

    }
    public override void UpdateState()
    {

    }
    public override void FixedUpdateState()
    {

    }
    public override PlayerStateMachine.EPlayerState GetNextState()
    {
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
