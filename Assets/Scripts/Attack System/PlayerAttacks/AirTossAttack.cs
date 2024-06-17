using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AirTossAttack : Attack 
{ 
    public AirTossAttack(float attackDuration, Collider2D attackArea, int damage, Vector3 offset) : base(attackDuration, attackArea, damage, offset)
    {

    }

    public override void StartAttack()
    {
        _attackArea.enabled = true;
    }

    public override void DoAttack(EnemyBehaviour enemy)
    {

    }

    public override void FinishAttack()
    {
        _attackArea.enabled = false;
    }

}

