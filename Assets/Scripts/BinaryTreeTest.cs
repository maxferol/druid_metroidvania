using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BinaryTreeTest : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        var testList = new List<int>() { 0, 1, 2, 3, 4, 5, 6, 7 };
        var testTree = new BinaryTree<int>(testList);
        LogTreeStart(testTree);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void LogTreeStart(BinaryTree<int> tree)
    {
        var log = "";
        var hasLeftSubtree = false;
        var hasRightSubtree = false;

        if (tree != null)
        {
            Debug.Log("tree started");

            if (tree.Left != null)
            {
                log += tree.Left.Data.ToString();
                hasLeftSubtree = true;
            }

            if (tree.Right != null)
            {
                log += tree.Right.Data.ToString();
                hasRightSubtree = true;
            }

            if (log == "")
                log = "no children";

            Debug.Log(log);

            if (hasLeftSubtree)
                LogTree(tree.Left);

            if (hasRightSubtree)
                LogTree(tree.Right);
        }
    }

    public void LogTree(BinaryTree<int> tree)
    {
        var log = "";
        var hasLeftSubtree = false;
        var hasRightSubtree = false;

        if (tree.Left != null)
        {
            log += tree.Left.Data.ToString();
            hasLeftSubtree = true;
        }

        if (tree.Right != null)
        {
            log += tree.Right.Data.ToString();
            hasRightSubtree = true;
        }

        if (log == "")
            log = "no children";

        Debug.Log(log);

        if (hasLeftSubtree)
            LogTree(tree.Left);

        if (hasRightSubtree)
            LogTree(tree.Right);
    }
}
