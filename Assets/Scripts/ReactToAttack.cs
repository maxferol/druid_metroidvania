using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ReactToAttack : MonoBehaviour
{
    public GameObject player;
    private FightSystem playerFightSystem;
    public string attackHitByName;
    public Attack attackHitBy;

    public Rigidbody2D rb;

    public Enemy hpSystem;

    public float attackTimer;
    public bool isTossed;

    public float lightAttackPower;
    public float heavyAttackPower;

    // Start is called before the first frame update
    void Start()
    {
        playerFightSystem = player.GetComponent<FightSystem>();
        attackTimer = 0;
        rb = gameObject.GetComponent<Rigidbody2D>();
        hpSystem = gameObject.GetComponent<Enemy>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void FixedUpdate()
    {
        React();
    }

    private void React()
    {
        if (attackHitBy == null)
            return;
        switch (attackHitByName)
        {
            case "BasicLightAttack":
                {
                    Debug.Log("enemy hit by BasicLightAttack");
                    ReactToLightAttack();
                    break;
                }
            case "BasicHeavyAttack":
                {
                    Debug.Log("enemy hit by BasicHeavyAttack");
                    ReactToHeavyAttack();
                    break;
                }
        }

        attackHitBy = null;
        attackHitByName = null;
    }

    public void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag.Equals("PlayerAttack"))
        {
            attackHitByName = collision.name;
            attackHitBy = playerFightSystem.triggerNameToAttack[attackHitByName];
            //attackHitBy.DoAttack(this);
        }
    }

    private void ReactToLightAttack()
    {
        hpSystem.AddDamage(attackHitBy._damage * (-1));

    }

    private void ReactToHeavyAttack()
    {
        hpSystem.AddDamage(attackHitBy._damage * (-1));
    }
}
