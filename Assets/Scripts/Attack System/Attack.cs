using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;

public abstract class Attack
{
    public float _attackDuration;
    public float _prepTime;
    public float _cdTime;
    public Collider2D _attackArea;
    public int _damage;
    public int _staggerPower;
    public Vector3 _offset;

    public Attack(float prepTime, float attackDuration, float cdTime, Collider2D attackArea, int damage, Vector3 offset)
    {
        _attackDuration = attackDuration;
        _prepTime = prepTime;
        _cdTime = cdTime;
        _attackArea = attackArea;
        _damage = damage;
        _offset = offset;
    }

    public abstract void StartAttack();
    public abstract void DoAttack(ReactToAttack enemy);
    public abstract void FinishAttack();

}
