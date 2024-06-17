using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraBehaviour : MonoBehaviour
{
    [SerializeField] private Vector3 offset;
    [SerializeField] private Transform target;
    [SerializeField] private float xLerpPower;
    [SerializeField] private float yLerpPower;

    private Vector3 newPos;

    private void Start()
    {
        transform.position = target.position + offset;
        newPos = transform.position;
    }

    void LateUpdate()
    {
        //newPos.x = Mathf.Lerp(transform.position.x, target.position.x + offset.x, xLerpPower);
        //newPos.y = Mathf.Lerp(transform.position.y, target.position.y + offset.y, yLerpPower);
        newPos = target.position + offset;
        transform.position = newPos;
    }
}
