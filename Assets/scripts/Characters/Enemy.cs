using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : Health
{
    [SerializeField]
    private float damage;

    public float Damage => damage;

}
