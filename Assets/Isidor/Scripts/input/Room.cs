using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(BoxCollider2D))]
public class Room : MonoBehaviour
{

    public Rect rect;
    new private BoxCollider2D collider;
    void Start()
    {
        collider = GetComponent<BoxCollider2D>();
        collider.isTrigger = true;
        rect = new Rect(this.collider.bounds.min, this.collider.bounds.max - this.collider.bounds.min);
    }

    private void OnTriggerEnter2D(Collider2D collider)
    {
        CameraController.Instance.PushRoom(this);
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        CameraController.Instance.PopRoom(this);
    }

    private void OnDrawGizmos()
    {
        if (collider == null) collider = GetComponent<BoxCollider2D>();
        Gizmos.color = Color.red;

        Gizmos.DrawWireCube(collider.bounds.center, collider.bounds.extents * 2);
    }
}
