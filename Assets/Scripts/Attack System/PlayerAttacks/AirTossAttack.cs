using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AirTossAttack : Attack 
{
    private float attackTimer;
    public float tossPower = 0.01f;
    public AirTossAttack(float attackDuration, float prepTime, float cdTime, Collider2D attackArea, int damage, Vector3 offset) : base(attackDuration, prepTime, cdTime, attackArea, damage, offset)
    {

    }

    public override void StartAttack()
    {
        attackTimer = 0;
        _attackArea.enabled = true;
    }

    public override void DoAttack(ReactToAttack enemy)
    {
        
    }

    public override void FinishAttack()
    {
        _attackArea.enabled = false;
    }

    IEnumerator AirToss(ReactToAttack enemy)
    {
        while (attackTimer < _attackDuration)
        {
            enemy.transform.position += new Vector3(0, 0.01f, 0);
            attackTimer += Time.fixedDeltaTime;
            yield return null;
        }
    }

}

