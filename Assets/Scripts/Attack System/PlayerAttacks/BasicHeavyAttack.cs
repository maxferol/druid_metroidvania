using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BasicHeavyAttack : Attack
{
    public BasicHeavyAttack(float prepTime, float attackDuration, float cdTime, Collider2D attackArea, int damage, Vector3 offset) : base(prepTime, attackDuration, cdTime, attackArea, damage, offset)
    {

    }

    public override void StartAttack()
    {
        _attackArea.enabled = true;
    }

    public override void DoAttack(ReactToAttack enemy)
    {

    }

    public override void FinishAttack()
    {
        _attackArea.enabled = false;
    }
}
