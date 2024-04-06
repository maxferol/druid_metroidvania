using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainCharacter : MonoBehaviour
{
    public Vector2 moveDirection;
    public float moveSpeed = 2f;
    public float jumpForce = 7f;
    public bool onGround;
    public Transform GroundCheck;
    public float checkRadius = 0.2f;
    public LayerMask Ground;
    public Transform Punch1;
    public float Punch1Radius;
    
    private Rigidbody2D rb;

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
        if(Input.GetKeyDown(KeyCode.F))
        {
            FightHandler.Action(Punch1.position, Punch1Radius, 7, 10, false);
        }
    }
}
