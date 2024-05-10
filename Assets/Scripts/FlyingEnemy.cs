using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlyingEnemy : MonoBehaviour
{
    [SerializeField]
    private float speed = 5;
    [SerializeField]
    private float followingRadius = 5;
    private Transform player;

    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").transform;
    }

    private void Update()
    {
        var playerDistance = Vector2.Distance(player.position, transform.position);
        if (playerDistance < followingRadius)
        {
            transform.position = Vector2.MoveTowards(this.transform.position, player.position, speed * Time.deltaTime);
        }
    }
}
