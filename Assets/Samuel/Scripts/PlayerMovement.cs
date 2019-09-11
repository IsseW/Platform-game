using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public float moveSpeed = 5f;
    private Rigidbody2D rb;
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }
    
    void Update()
    {
        Jump();
        rb.position += new Vector2(InputSystem.Horizontal() * moveSpeed * Time.deltaTime, 0f);
    }
    
    void Jump()
    {
        if(InputSystem.JumpDown())
        {
            rb.AddForce(new Vector2(0f, 5f), ForceMode2D.Impulse);
        }
        
    }
}
