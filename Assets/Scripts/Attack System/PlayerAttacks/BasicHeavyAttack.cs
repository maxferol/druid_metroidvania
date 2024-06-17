using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BasicHeavyAttack : Attack
{
    public BasicHeavyAttack(float attackDuration, Collider2D attackArea, int damage, Vector3 offset) : base(attackDuration, attackArea, damage, offset)
    {

    }

    public override void StartAttack()
    {
        _attackArea.enabled = true;
    }

    public override void DoAttack(EnemyBehaviour enemy)
    {
        enemy.hp -= _damage;
    }

    public override void FinishAttack()
    {
        _attackArea.enabled = false;
    }
}
