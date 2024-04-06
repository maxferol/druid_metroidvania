using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Health : MonoBehaviour
{
    private float hp;

    [SerializeField]
    private float maxHp;

    [SerializeField]
    private UnityEvent die;

    [SerializeField]
    private UnityEvent<float> hpChnged;

    public float HeatPoints
    {
        get { return hp; }
        set
        {
            hp = value;
            hpChnged?.Invoke(hp);

            if (hp <= 0)
                die?.Invoke();

        }
    }

    public void init()
    {
        HeatPoints = maxHp;
    }
    public void getDamage(float damage) {
        HeatPoints -= damage;
    }

    private void Start()
    {
        init();
    }

}
