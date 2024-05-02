using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class knockObject : MonoBehaviour
{
    private float knockBackPower = Knock.moveSpeed * 12;
    public float knockBackDuration;

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.gameObject.tag == "Player")
        {
            StartCoroutine(Knock.instance.KnockBack(knockBackDuration, knockBackPower, this.transform));
        }
    }
}
