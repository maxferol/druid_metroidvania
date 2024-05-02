using System;
using System.Collections;
using UnityEngine;
using UnityEngine.Serialization;

public class MainCharacter : MonoBehaviour
{
    // Настройки горизонтального движения
    [SerializeField] private Vector2 moveDirection;
    [SerializeField] private bool isFacedRight = true;
    [SerializeField] private float moveSpeed = 0.1f;
    
    // Настройки вертикального движения
    [SerializeField] private float jumpForce = 10f;
    [SerializeField] private bool onGround;
    [SerializeField] private Transform groundCheck;
    [SerializeField] private LayerMask ground;
    [SerializeField] private float checkRadius = 0.2f;
    [SerializeField] private int currentJumps = 0;
    [SerializeField] private int maximumJumps = 2;
    [SerializeField] private TimeSpan jumpCooldown = TimeSpan.FromSeconds(1);
    [SerializeField] private DateTime lastJumped = DateTime.Now;
    [SerializeField] private float wallCheckRadius = 0.2f;
    [SerializeField] private bool isWallSliding = false;
    [SerializeField] private float wallSlideSpeed = 2f;
    [SerializeField] private Transform wallCheck;
    [SerializeField] private LayerMask wallLayer;
    [SerializeField] private float fallForce = 5f;
    [SerializeField] private DateTime lastFalled = DateTime.Now;
    [SerializeField] private TimeSpan fallCooldown = TimeSpan.FromSeconds(2);
    
    // Настройки атаки
    [SerializeField] private int punch1Force = 10;
    [SerializeField] private int punch2Force = 20;
    [SerializeField] private TimeSpan punch1Cooldown = TimeSpan.FromSeconds(1);
    [SerializeField] private TimeSpan punch2Cooldown = TimeSpan.FromSeconds(2);
    [SerializeField] private Transform punch1;
    [SerializeField] private float punch1Radius;
    [SerializeField] private Transform punch2;
    [SerializeField] private float punch2Radius;
    
    //Настройки рывка
    [SerializeField] private float lungeForce = 5f;
    [SerializeField] private float lungeTimeInSeconds = 0.5f;
    [SerializeField] private bool canLunge = true;
    [SerializeField] private bool isLunging = false;
    [SerializeField] private float lungeCooldownInSeconds = 2;

    [SerializeField] private LayerMask enemyLayer;

    private Rigidbody2D _rb;
    private DateTime _punch1LastUsed = DateTime.Now;
    private DateTime _punch2LastUsed = DateTime.Now;

    public void Start()    
    {
        _rb = GetComponent<Rigidbody2D>();
    }
    public void Update()
    {
        if (isLunging)
        {
            return;
        }
    }

    public void FixedUpdate()
    {
        if (isLunging)
        {
            return;
        }
        
        Move();
        Jump();
        CheckingGround();
        Fight();
        WallSlide();
        Flip();
        Fall();
        
        if (Input.GetKeyDown(KeyCode.LeftShift) && canLunge)
        {
            StartCoroutine(Lunge());
        }
    }

    private void Move()
    {
        moveDirection.x = Input.GetAxisRaw("Horizontal");
        _rb.position += new Vector2(moveDirection.x * moveSpeed, 0);    
    }

    private void Jump()
    {
        if (onGround)
        {
            currentJumps = 0;    
        }
        
        if (Input.GetButton("Jump") && (onGround || (currentJumps < maximumJumps && lastJumped + jumpCooldown <= DateTime.Now)))
        {
            _rb.velocity = Vector2.up * jumpForce;
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
        if(Input.GetButton("Fire1") && _punch1LastUsed + punch1Cooldown <= DateTime.Now)
        {
            _punch1LastUsed = DateTime.Now;
            FightHandler.Fight(punch1.position, punch1Radius, enemyLayer, punch1Force);
        }
        
        if(Input.GetButton("Fire2") && _punch2LastUsed + punch2Cooldown <= DateTime.Now)
        {
            _punch2LastUsed = DateTime.Now;
            FightHandler.Fight(punch2.position, punch2Radius, enemyLayer, punch2Force);
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

    private IEnumerator Lunge()
    {
        canLunge = false;
        isLunging = true;
        var gravity = _rb.gravityScale;
        _rb.gravityScale = 0f;
        _rb.velocity = new Vector2(transform.localScale.x * lungeForce, 0);
        yield return new WaitForSeconds(lungeTimeInSeconds);
        _rb.gravityScale = gravity;
        isLunging = false;
        yield return new WaitForSeconds(lungeCooldownInSeconds);
        canLunge = true;
    }

    private bool IsOnWall()
    {
        return Physics2D.OverlapCircle(wallCheck.position, wallCheckRadius, wallLayer);
    }

    private void WallSlide()
    {
        if (IsOnWall() && !onGround)
        {
            currentJumps = 0; 
            isWallSliding = true;
            _rb.velocity = new Vector2(_rb.velocity.x, Mathf.Clamp(_rb.velocity.y, -wallSlideSpeed, float.MaxValue));
        }
        else
        {
            isWallSliding = false;
        }
    }

    private void Fall()
    {
        if (Input.GetKeyDown(KeyCode.S) && lastFalled + fallCooldown <= DateTime.Now)
        {
            lastFalled = DateTime.Now;
            _rb.velocity += Vector2.down * fallForce;
        }
    }
}
