using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Knock : MonoBehaviour

{
    public static float moveSpeed = 8;
    public static Knock instance; 
    private Rigidbody2D rb;
    private Vector2 move;
    private bool facingRight = true;

    private void Awake()
    {
        instance = this;
    }

    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    private void Update()
    {
        move.x = Input.GetAxisRaw("Horizontal");
        move.Normalize();
        rb.velocity = move * moveSpeed;
        if (!facingRight && move.x > 0)
        {
            Flip();
        }
        else if (facingRight && move.x < 0)
        {
            Flip();
        }

    }
    public IEnumerator KnockBack(float knockBackDuration, float knockBackPower, Transform obj)
    {
        float timer = 0;

        while(knockBackDuration > timer)
        {
            timer += Time.deltaTime;
            Vector2 direction = (obj.position - transform.position).normalized;
            rb.AddForce(-direction * knockBackPower);
        }
        yield return 0;
    }

    void Flip()
    {
        facingRight = !facingRight;
        Vector3 Scaler = transform.localScale;
        Scaler.x *= -1;
        transform.localScale = Scaler;

    }
}
