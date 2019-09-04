using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
[RequireComponent(typeof(Collider2D))]
public class Activator : MonoBehaviour
{
    public CollisionEvent enter;
    public CollisionEvent exit;

    private void Start()
    {
        if (enter == null) enter = new CollisionEvent();
        if (exit == null) exit = new CollisionEvent();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        enter.Invoke(collision);
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        exit.Invoke(collision);
    }
}

public class CollisionEvent : UnityEvent<Collider2D>
{
    public CollisionEvent() : base()
    {

    }
}
