using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class Health : MonoBehaviour
{
    [SerializeField] private int health = 100;

    private int MAX_HEALTH = 100;

    void Update()
    {
    }

    public void Damage(int amount)
    {
        this.health -= amount;

        if (health <= 0)
        {
            Die();
        }
    }

    public void Heal(int amount)
    {
        this.health = Math.Min(MAX_HEALTH, health + amount);
    }

    private void Die()
    {
        Destroy(gameObject);
    }
}