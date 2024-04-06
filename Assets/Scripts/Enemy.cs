using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    public float HP = 100;
    
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    
    public void AddDamage(float damage)
    {
        HP += damage;

        if(HP <= 0)
        {
            HP = 0;
            Destroy(gameObject);
        }
    }
}
