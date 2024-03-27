using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainCharacter : MonoBehaviour
{
    public float horizontalSpeed = 0.1f;
    public float jumpForce = 0.1f;
    
    private Rigidbody2D rb;

    public void Start()    
    {
        rb = GetComponent<Rigidbody2D>();
    }
    public void Update()
    {
        var horizontal = Input.GetAxis("Horizontal");
        transform.position += new Vector3(horizontal, 0, 0) * horizontalSpeed * Time.deltaTime;
        if (Input.GetAxis("Jump") != 0f)
        {
            rb.AddForce(new Vector3(0, jumpForce, 0), ForceMode2D.Impulse);   
        }
    }
}
