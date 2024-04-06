using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class MainHero : Health
{
    [SerializeField]
    private UnityEvent<float> gotDamage;

    private void damaged(Collision other)
    {
        if(other.collider.TryGetComponent<Enemy>(out var enemy))
        {
            gotDamage?.Invoke(enemy.Damage);
        }
    }
   
}
