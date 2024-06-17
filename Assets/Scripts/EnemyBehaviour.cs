using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBehaviour : MonoBehaviour
{
    public GameObject player;
    private FightSystem playerFightSystem;
    public string attackHitByName;
    public Attack attackHitBy;

    public int hp;

    // Start is called before the first frame update
    void Start()
    {
        playerFightSystem = player.GetComponent<FightSystem>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void FixedUpdate()
    {
        ReactToAttack();
    }

    private void ReactToAttack()
    {
        //if (attackHitBy == null)
        //    return;
        //switch (attackHitByName)
        //{
        //    case "BasicLightAttack":
        //        {
        //            Debug.Log("enemy hit by BasicLightAttack");
        //            break;
        //        }
        //}

        //attackHitBy = null;
        //attackHitByName = null;
    }

    public void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag.Equals("PlayerAttack"))
        {
            attackHitByName = collision.name;
            attackHitBy = playerFightSystem.triggerNameToAttack[attackHitByName];
            attackHitBy.DoAttack(this);
        }
    }
}
