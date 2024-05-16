using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PatrollingEnemy : MonoBehaviour
{
    [SerializeField] protected float speed = 2;
    [SerializeField] private float patrolRadius = 2;
    [SerializeField] private Transform startPoint;
    [SerializeField] private bool isMovingRight = false;
    [SerializeField] private float stoppingDistance = 1;

    protected Transform player;
    protected bool isAngry = false;
    protected bool isChilling = false;
    protected bool isGoingBack = false;

    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").transform;       
    }

    // Update is called once per frame
    void Update()
    {
        if (Vector2.Distance(transform.position, startPoint.position) < patrolRadius && !isAngry)
        {
            isChilling = true;
        }

        if (Vector2.Distance(transform.position, player.position) < stoppingDistance)
        {
            isAngry = true;
            isChilling = false;
            isGoingBack = false;
        }

        if (Vector2.Distance(transform.position, player.position) > stoppingDistance)
        {
            isAngry = false;
            isGoingBack = true;
        }

        if (isChilling)
        {
            Chill();
        }
        else if (isAngry)
        {
            Attack();
        }
        else if (isGoingBack)
        {
            GoBackToPoint();
        }

    }

    protected virtual void Chill()
    {
        if (transform.position.x > startPoint.position.x + patrolRadius)
        {
            isMovingRight = false;
        }
        else if (transform.position.x < startPoint.position.x - patrolRadius)
        {
            isMovingRight = true;
        }

        if (isMovingRight)
        {
            transform.position = new Vector2(transform.position.x + speed * Time.deltaTime, transform.position.y);
        }
        else 
        {
            transform.position = new Vector2(transform.position.x - speed * Time.deltaTime, transform.position.y);
        }
    }

    protected virtual void Attack()
    {
        transform.position = Vector2.MoveTowards(transform.position, player.position, speed * Time.deltaTime);
    }

    protected virtual void GoBackToPoint()
    {
        transform.position = Vector2.MoveTowards(transform.position, startPoint.position, speed * Time.deltaTime);
    }
}
