using System;
using UnityEngine;

public class KinematicMovement : MonoBehaviour
{
    public float runSpeed;
    public bool isRunning = false;
    public bool isFacedRight = true;

    public bool isOnGround = false;
    [SerializeField] private LayerMask ground;
    [SerializeField] private float runDirection;

    [SerializeField] private bool jumpPressed;
    public float jumpSpeed;
    public bool isJumping;
    public int jumpsAvailable;
    public int maxJumps;
    public float currentJumpTime;
    public float maxJumpTime;

    public float fallSpeed;
    public bool isFalling;

    [SerializeField] private bool dashPressed;
    public bool isDashing;
    public float dashSpeed;
    public float dashDuration;
    public float dashCooldown;

    [SerializeField] private float xDif;
    [SerializeField] private float yDif;

    private Transform plTransform;
    private Rigidbody2D plRb;

    public void Start()
    {
        plTransform = GetComponent<Transform>();
        plRb = GetComponent<Rigidbody2D>();
    }

    public void Update()
    {
        GetHorizontalInput();
        GetVerticalInput();
    }

    public void FixedUpdate()
    {
        CheckGround();
        CheckHorizontalMove();
        CheckVerticalMove();
        ChangePos();
    }

    private void OnDrawGizmos()
    {
        var plPos = plTransform.position;
        Gizmos.DrawLine(plPos + new Vector3(0.5f, 0, 0), plPos + new Vector3(0.5f, -0.125f, 0));
        Gizmos.DrawLine(plPos + new Vector3(-0.5f, 0, 0), plPos + new Vector3(-0.5f, -0.125f, 0));
    }

    private void TryFlip()
    {
        if ((runDirection > 0 && !isFacedRight) || (runDirection < 0 && isFacedRight))
        {
            var newDirection = transform.localScale;
            newDirection.x *= -1;
            transform.localScale = newDirection;
            isFacedRight = !isFacedRight;
        }
    }

    private void GetHorizontalInput()
    {
        runDirection = Input.GetAxisRaw("Horizontal");
        dashPressed = Input.GetButton("Dash");
    }

    private void CheckHorizontalMove()
    {
        if (dashPressed && dashCooldown == 0)
        {
            isDashing = true;
        }

        if (runDirection != 0)
        {
            isRunning = true;
            xDif = runDirection * runSpeed;
            TryFlip();
        }

        else
        {
            isRunning = false;
            xDif = 0;
        }
    }

    private void ChangePos()
    {
        if (xDif != 0 || yDif != 0)
            plRb.MovePosition(plRb.position + new Vector2(xDif, yDif));
    }

    private void CheckGround()
    {
        var plPos = plTransform.position;
        var leftRayOrigin = new Vector2(plPos.x - 0.5f, plPos.y);
        var rightRayOrigin = new Vector2(plPos.x + 0.5f, plPos.y);
        var leftHit = Physics2D.Raycast(leftRayOrigin, Vector2.down, 0.125f, ground);
        var rightHit = Physics2D.Raycast(rightRayOrigin, Vector2.down, 0.125f, ground);
        if (leftHit || rightHit)
        {
            isOnGround = true;
            isFalling = false;
            Debug.Log("Is on ground");
        }
        else
            isOnGround = false;
    }

    private void GetVerticalInput()
    {
        jumpPressed = Input.GetButton("Jump");
    }

    private void CheckVerticalMove()
    {
        if (isOnGround)
        {
            jumpsAvailable = maxJumps;
        }

        if (jumpPressed)
        {
            if ((isOnGround || isFalling) && jumpsAvailable > 0)
            {
                isJumping = true;
                isFalling = false;
                yDif = jumpSpeed;
                jumpsAvailable -= 1;
            }
        }

        else
            isJumping = false;

        if (!isOnGround && !isJumping)
        {
            isFalling = true;
            yDif = -fallSpeed;
        }

        if (!isFalling && !isJumping)
            yDif = 0;
    }

    private void Jump()
    {

    }
}
