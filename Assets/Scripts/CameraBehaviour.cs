using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraBehaviour : MonoBehaviour
{
    public Transform plTransform;
    // Start is called before the first frame update
    void Start()
    {
        transform.position = new Vector3(plTransform.position.x, plTransform.position.y, -10);
    }

    // Update is called once per frame
    void LateUpdate()
    {
        transform.position = new Vector3(plTransform.position.x, plTransform.position.y + 4, -10);
    }
}
