using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharMoveTest : MonoBehaviour
{
    public Vector3 prevPos;
    void LateUpdate()
    {
        if (prevPos != null)
        {
            Debug.Log(transform.position.x - prevPos.x);
            prevPos = transform.position;
        }
        else
            prevPos = transform.position;
    }
}
