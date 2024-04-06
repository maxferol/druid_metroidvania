using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainCharacter : MonoBehaviour
{
    public Vector2 moveDirection;
    public float moveSpeed = 2f;
    public float jumpForce = 30f;
    public bool onGround;
    public Transform GroundCheck;
    public float checkRadius = 0.2f;
    public LayerMask Ground;
    public Transform Punch1;
    public TimeSpan Punch1Cooldown = TimeSpan.FromSeconds(1);
    public float Punch1Radius;
    public Transform Punch2;
    public float Punch2Radius;
    public TimeSpan Punch2Cooldown = TimeSpan.FromSeconds(2);
    
    private Rigidbody2D rb;
    private DateTime punch1LastUsed = DateTime.Now;
    private DateTime punch2LastUsed = DateTime.Now;

    public void Start()    
    {
        rb = GetComponent<Rigidbody2D>();
    }
    public void Update()
    {
        Move();
        Jump();
        CheckingGround();
        Fight();
    }

    private void Move()
    {
        moveDirection.x = Input.GetAxis("Horizontal");
        rb.velocity = new Vector2(moveDirection.x * moveSpeed, rb.velocity.y);    
    }

    private void Jump()
    {
        if (Input.GetAxis("Jump") != 0f && onGround)
        {
            rb.AddForce(Vector2.up * jumpForce);
        }
    }
    
    private void CheckingGround()
    {
        onGround = Physics2D.OverlapCircle(GroundCheck.position, checkRadius, Ground);
    }

    private void Fight()
    {
        if(Input.GetAxis("Fire1") != 0f && punch1LastUsed + Punch1Cooldown <= DateTime.Now)
        {
            punch1LastUsed = DateTime.Now;
            FightHandler.Action(Punch1.position, Punch1Radius, 7, 10, false);
        }
        
        if(Input.GetAxis("Fire2") != 0f && punch2LastUsed + Punch2Cooldown <= DateTime.Now)
        {
            punch2LastUsed = DateTime.Now;
            FightHandler.Action(Punch2.position, Punch2Radius, 7, 20, false);
        }
    }
}
