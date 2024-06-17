using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BinaryTree<T>
{
    public BinaryTree<T> Left;
    public BinaryTree<T> Right;
    public T Data;

    public BinaryTree(List<T> list)
    {
        if (list.Count != 0)
            ConstructSubTree(list, 0);
    }

    private BinaryTree(List<T> list, int index)
    {
        ConstructSubTree(list, index);
    }

    private void ConstructSubTree(List<T> list, int index)
    {
        Data = list[index];
        if (index * 2 + 1 < list.Count)
            Left = new BinaryTree<T>(list, index * 2 + 1);
        else
            Left = null;
        if (index * 2 + 2 < list.Count)
            Right = new BinaryTree<T>(list, index * 2 + 2);
        else
            Right = null;
    }
}

public class BinaryTree<T1, T2>
{
    public BinaryTree<T1, T2> Left;
    public BinaryTree<T1, T2> Right;
    public T2 LeftWeight;
    public T2 RightWeight;
    public T1 Data;
}
