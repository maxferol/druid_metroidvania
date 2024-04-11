using System;
using UnityEngine;

public class MainCharacter : MonoBehaviour
{
    // Настройки горизонтального движения
    public Vector2 moveDirection;
    public bool isFacedRight = true;
    public float moveSpeed = 2f;
    
    // Настройки вертикального движения
    public float jumpForce = 10f;
    public bool onGround;
    public Transform groundCheck;
    public LayerMask ground;
    public float checkRadius = 0.2f;
    public int currentJumps = 0;
    public int maximumJumps = 2;
    public DateTime lastJumped = DateTime.Now;
    public TimeSpan jumpCooldown = TimeSpan.FromSeconds(1);
    
    // Настройки атаки
    public int Punch1Force = 10;
    public int Punch2Force = 20;
    public TimeSpan Punch1Cooldown = TimeSpan.FromSeconds(1);
    public TimeSpan Punch2Cooldown = TimeSpan.FromSeconds(2);
    public Transform Punch1;
    public float Punch1Radius;
    public Transform Punch2;
    public float Punch2Radius;
    
    private Rigidbody2D rb;
    private DateTime punch1LastUsed = DateTime.Now;
    private DateTime punch2LastUsed = DateTime.Now;

    public void Start()    
    {
        rb = GetComponent<Rigidbody2D>();
    }
    public void Update()
    {
    }

    public void FixedUpdate()
    {
        Move();
        Jump();
        CheckingGround();
        Fight();
        Flip();
    }

    private void Move()
    {
        moveDirection.x = Input.GetAxisRaw("Horizontal");
        rb.velocity = new Vector2(moveDirection.x * moveSpeed, rb.velocity.y);    
    }

    private void Jump()
    {
        if (onGround)
        {
            currentJumps = 0;    
        }
        
        if (Input.GetButton("Jump") && (onGround || (currentJumps < maximumJumps && lastJumped + jumpCooldown <= DateTime.Now)))
        {
            rb.velocity = Vector2.up * jumpForce;
            lastJumped = DateTime.Now;
            currentJumps++;
        }
        
    }
    
    private void CheckingGround()
    {
        onGround = Physics2D.OverlapCircle(groundCheck.position, checkRadius, ground);
    }

    private void Fight()
    {
        if(Input.GetButton("Fire1") && punch1LastUsed + Punch1Cooldown <= DateTime.Now)
        {
            punch1LastUsed = DateTime.Now;
            FightHandler.Fight(Punch1.position, Punch1Radius, (int)LayersNumbers.Enemy, Punch1Force);
        }
        
        if(Input.GetButton("Fire2") && punch2LastUsed + Punch2Cooldown <= DateTime.Now)
        {
            punch2LastUsed = DateTime.Now;
            FightHandler.Fight(Punch2.position, Punch2Radius, (int)LayersNumbers.Enemy, Punch2Force);
        }
    }

    private void Flip()
    {
        if (moveDirection.x > 0 && !isFacedRight || moveDirection.x < 0 && isFacedRight)
        {
            var newDirection = transform.localScale;
            newDirection.x *= -1;
            transform.localScale = newDirection;
            isFacedRight = !isFacedRight;
        }
    }
}
