using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerStateMachine : StateMachine<PlayerStateMachine.EPlayerState>
{
    public enum EPlayerState
    {
        Idle,
        Running,
        Dashing,
        Jumping,
        Falling,
        WallSliding,
    }

    public PlayerContext context;

    private Transform plTransform;
    private Rigidbody2D plRB;

    [SerializeField] private LayerMask ground;
    [SerializeField] private int wallsLayer;

    [SerializeField] private float heightAboveGround;

    [SerializeField] private float runSpeed;

    [SerializeField] private float jumpSpeed;
    [SerializeField] private float jumpMaxDuration;
    [SerializeField] private int jumpsMaxNumber;
    [SerializeField] private int jumpsLeft;

    [SerializeField] private float fallingSpeed;

    [SerializeField] private float dashDuration;
    [SerializeField] private float dashSpeed;
    [SerializeField] private float dashCooldown;

    [SerializeField] private float wallSlidingSpeed;
    [SerializeField] private float jumpingFromWallSpeed;
    [SerializeField] private float jumpingFromWallMinTime;

    private void Awake()
    {
        plTransform = GetComponent<Transform>();
        plRB = GetComponent<Rigidbody2D>();
        wallsLayer = LayerMask.NameToLayer("Walls");
        context = new PlayerContext(runSpeed, dashDuration, dashSpeed, dashCooldown,
            jumpSpeed, jumpMaxDuration, jumpsMaxNumber, jumpsLeft, 
            fallingSpeed, wallSlidingSpeed, jumpingFromWallSpeed, jumpingFromWallMinTime, 
            ground, heightAboveGround, plTransform, plRB);
        InitializeStates();
    }

    private void InitializeStates()
    {
        States.Add(EPlayerState.Idle, new IdleState(context, EPlayerState.Idle));
        States.Add(EPlayerState.Running, new RunningState(context, EPlayerState.Running));
        States.Add(EPlayerState.Dashing, new DashingState(context, EPlayerState.Dashing));
        States.Add(EPlayerState.Jumping, new JumpingState(context, EPlayerState.Jumping));
        States.Add(EPlayerState.Falling, new FallingState(context, EPlayerState.Falling));
        States.Add(EPlayerState.WallSliding, new WallSlidingState(context, EPlayerState.WallSliding));
        CurrentState = States[EPlayerState.Idle];
    }

    public override void SelfUpdate()
    {
        context.UpdateInputContext();
    }

    public override void SelfFixedUpdate()
    {
        //context.UpdateInputContext();
        context.UpdatePhysicsContext();
    }

    private void OnDrawGizmos()
    {
        var plPos = GetComponent<Transform>().position;
        Gizmos.DrawLine(plPos + new Vector3(0.5f, 0, 0), plPos + new Vector3(0.5f, -0.25f, 0));
        Gizmos.DrawLine(plPos + new Vector3(-0.5f, 0, 0), plPos + new Vector3(-0.5f, -0.25f, 0));
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.layer == wallsLayer)
        {
            context.hitsWall = true;
            context._wallCollision = collision;
            context.CheckWallSide();
            //if (context.isWallRightSide)
                //Debug.Log("Right Wall hit");
            //else
                //Debug.Log("Left Wall hit");
        }
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.gameObject.layer == wallsLayer)
        {
            context.hitsWall = false;
            context._wallCollision = null;
            //Debug.Log("Wall hit stopped");
        }
    }
}
