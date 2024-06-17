using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class PlayerContext
{
    public float _runSpeed;

    public float _jumpSpeed;
    public float _jumpMaxDuration;
    public int _jumpsMaxNumber;
    public int _jumpsLeft;

    public float _fallingSpeed;

    public float _dashDuration;
    public float _dashCooldown;
    public float _dashCooldownLeft = 0;
    public float _dashSpeed;

    public bool dashPressed;
    public bool jumpPressed = false;
    public bool jumpIsHeld;
    public float runDirection;

    public bool lightAttackPressed;
    public bool heavyAttackPressed;

    public bool isOnGround;
    public float groundLevel;
    public float _heightAboveGround;

    public bool hitsWall;
    public float _wallSlidingSpeed;
    public Collision2D _wallCollision;
    public bool isWallRightSide;
    public float _jumpingFromWallSpeed;
    public bool jumpingFromWall;
    public float _jumpingFromWallMinTime;

    private float _allowedAttackLag;
    private float lightAttackTimer;
    private float rightAttackTimer;

    public LayerMask _ground;

    public Transform _plTransform;
    public Rigidbody2D _plRB;
    public GameObject _player;
    public FightSystem _fightSystem;

    public PlayerContext(float runSpeed, float dashDuration, float dashSpeed, float dashCooldown,
        float jumpSpeed, float jumpMaxDuration, int jumpsMaxNumber, int jumpsLeft, 
        float fallingSpeed, float wallSlidingSpeed, float jumpingFromWallSpeed, float jumpingFromWallMinTime,
        LayerMask ground, float heightAboveGround, Transform plTransform, Rigidbody2D plRB, GameObject player, FightSystem fightSystem,
        float allowedAttackLag)
    {
        _runSpeed = runSpeed;
        _dashDuration = dashDuration;
        _dashSpeed = dashSpeed;
        _dashCooldown = dashCooldown;
        _jumpSpeed = jumpSpeed;
        _jumpMaxDuration = jumpMaxDuration;
        _jumpsMaxNumber = jumpsMaxNumber;
        _jumpsLeft = jumpsLeft;
        _fallingSpeed = fallingSpeed;
        _wallSlidingSpeed = wallSlidingSpeed;
        _jumpingFromWallSpeed = jumpingFromWallSpeed;
        _jumpingFromWallMinTime = jumpingFromWallMinTime;
        _ground = ground;
        _heightAboveGround = heightAboveGround;
        _plTransform = plTransform;
        _plRB = plRB;
        _player = player;
        _fightSystem = fightSystem;
        _allowedAttackLag = allowedAttackLag;
    }

    public void UpdateInputContext()
    {
        dashPressed = Input.GetButton("Dash");
        jumpPressed = jumpPressed || Input.GetButtonDown("Jump");
        jumpIsHeld = Input.GetButton("Jump");
        runDirection = Input.GetAxisRaw("Horizontal");
        lightAttackPressed = Input.GetMouseButton(0);
        heavyAttackPressed = Input.GetMouseButton(1);
    }

    public void UpdatePhysicsContext()
    {
        isOnGround = CheckGround();
        //CheckWallSide();
        if (_dashCooldownLeft > 0)
            _dashCooldownLeft -= Time.fixedDeltaTime;
    }

    private bool CheckGround()
    {
        var plPos = _plTransform.position;
        var leftRayOrigin = new Vector2(plPos.x - 0.5f, plPos.y);
        var rightRayOrigin = new Vector2(plPos.x + 0.5f, plPos.y);
        var leftHitCheck = Physics2D.Raycast(leftRayOrigin, Vector2.down, 0.25f, _ground);
        var rightHitCheck = Physics2D.Raycast(rightRayOrigin, Vector2.down, 0.25f, _ground);
        var leftHitSecured = Physics2D.Raycast(leftRayOrigin, Vector2.down, 0.1f, _ground);
        var rightHitSecured = Physics2D.Raycast(rightRayOrigin, Vector2.down, 0.1f, _ground);
        if (leftHitCheck)
        {
            if (rightHitCheck)
                groundLevel = Mathf.Max(leftHitCheck.point.y, rightHitCheck.point.y) + _heightAboveGround;
            else
                groundLevel = leftHitCheck.point.y + _heightAboveGround;
        }
        else
        {
            if (rightHitCheck)
                groundLevel = rightHitCheck.point.y + _heightAboveGround;
            else
                groundLevel = float.NaN;
        }
        return (leftHitSecured || rightHitSecured);
    }

    public void CheckWallSide()
    {
        if (_wallCollision.gameObject.transform.position.x > _plTransform.position.x)
            isWallRightSide = true;
        else
            isWallRightSide = false;
    }
}
