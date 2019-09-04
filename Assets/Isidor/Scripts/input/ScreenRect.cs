using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(BoxCollider2D))]
public class ScreenRect : MonoBehaviour
{

    BoxCollider2D rect;
    void Start()
    {
        rect = GetComponent<BoxCollider2D>();
        rect.isTrigger = true;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        CameraController.Instance.SetRoom(rect);
    }

    private void OnDrawGizmos()
    {
        if (rect == null) rect = GetComponent<BoxCollider2D>();
        Gizmos.color = Color.red;

        Gizmos.DrawWireCube(rect.bounds.center, rect.bounds.extents * 2);
    }
}
