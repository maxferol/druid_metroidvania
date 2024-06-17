using System.Collections;
using System.Collections.Generic;
using Unity.Burst.CompilerServices;
using UnityEngine;

public class Turret : MonoBehaviour
{
    public GameObject bullet;
    public GameObject bulletParent;

    [SerializeField]
    private float fireRate = 1;
    [SerializeField]
    private float nextFireTime;
    [SerializeField]
    private float shootingRadius = 5;
    [SerializeField]
    private float speed = 5;
    [SerializeField]
    private float followingRadius = 10;
    private Transform player;

    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").transform;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        var playerDistance = Vector2.Distance(player.position, transform.position);
        if (playerDistance <= shootingRadius && nextFireTime < Time.time)
        {
            Instantiate(bullet, bulletParent.transform.position, Quaternion.identity);
            nextFireTime = Time.time + fireRate;
        }

        //!Physics.Raycast(transform.position, player.transform.position, Mathf.Infinity, ~ignoreLayer)
    }
}
