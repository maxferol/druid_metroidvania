using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;

public class FightTree
{
    public PlayerContext playerContext;

    public Attack currentAttack;

    public FightTree Left;
    public FightTree Right;
    public FightTreeNode Data;

    public FightTree(List<FightTreeNode> list)
    {
        if (list.Count != 0)
            ConstructSubTree(list, 0);
    }

    private FightTree(List<FightTreeNode> list, int index)
    {
        ConstructSubTree(list, index);
    }

    private void ConstructSubTree(List<FightTreeNode> list, int index)
    {
        Data = list[index];
        if (index * 2 + 1 < list.Count)
            Left = new FightTree(list, index * 2 + 1);
        else
            Left = null;
        if (index * 2 + 2 < list.Count)
            Right = new FightTree(list, index * 2 + 2);
        else
            Right = null;
    }
}
