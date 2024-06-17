using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FightTreeNode
{
    public Attack _attack;
    public float _minTiming;
    public float _maxTiming;

    public FightTreeNode(Attack attack, float minTiming, float maxTiming)
    {
        _attack = attack;
        _minTiming = minTiming;
        _maxTiming = maxTiming;
    }


}
