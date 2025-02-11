using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackArea : MonoBehaviour
{
    [SerializeField]
    private int damage = 3;

    private void OnTriggerEnter2D(Collider2D collider)
    {
        if (collider.GetComponent<Health>() != null && collider.tag == "Player")
        {
            Health health = collider.GetComponent<Health>();
            health.Damage(damage);
        }
    }
}