using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : Thing
{
    public Movement movement;
    void Start()
    {
        
    }
    
    void Update()
    {
        
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.tag == "Room")
        {
            Debug.Log(collision.name);
            Destroy(gameObject);
        }
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.tag == "Ground")
        {
            Debug.Log(collision.name);
            Destroy(gameObject);
        }
    }
}
