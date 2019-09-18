using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public float moveSpeed = 5f;
    private Rigidbody2D rb;
    public bool isGrounded = false;
    public float jumpForce = 5f;
    public Transform firePoint;
    public LinearMovement bullet;
    private bool m_FacingRight;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }
    
    void Update()
    {
        float direction = InputSystem.Horizontal() * moveSpeed * Time.deltaTime;

        Jump();

        rb.position += new Vector2(direction, 0f);

        if(direction > 0 && m_FacingRight)
        {
            Flip();
        }
        else if (direction < 0 && !m_FacingRight)
        {
            Flip();
        }

        if (InputSystem.AttackDown())
        {
            Shoot();
        }

    }
    
    void Jump()
    {
        if(InputSystem.JumpDown() && isGrounded == true)
        {
            rb.AddForce(new Vector2(0f, jumpForce), ForceMode2D.Impulse);
        }
        
    }

    void Shoot()
    {
        LinearMovement t = Instantiate(bullet, firePoint.position, firePoint.rotation);
        t.startPos = firePoint.position;
        t.direction = firePoint.right;
        t.StartMovement();
    }

    private void Flip()
    {
        m_FacingRight = !m_FacingRight;

        transform.Rotate(0f, 180f, 0f);
    }
}
