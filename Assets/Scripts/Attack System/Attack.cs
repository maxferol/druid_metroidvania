using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;

public abstract class Attack
{
    public float _attackDuration;
    public Collider2D _attackArea;
    public int _damage;
    public int _staggerPower;
    public Vector3 _offset;

    public Attack(float attackDuration, Collider2D attackArea, int damage, Vector3 offset)
    {
        _attackDuration = attackDuration;
        _attackArea = attackArea;
        _damage = damage;
        _offset = offset;
    }

    public abstract void StartAttack();
    public abstract void DoAttack(EnemyBehaviour enemy);
    public abstract void FinishAttack();

}
