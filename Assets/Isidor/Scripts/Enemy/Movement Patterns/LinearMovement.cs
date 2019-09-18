using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LinearMovement : Movement
{
    public Vector2 startPos;
    public Vector2 direction;
    public float speed;

    public override Vector2 GetPosition(float time)
    {
        return startPos + direction.normalized * time * speed;
    }

    public override Vector2 GetPosition()
    {
        return transform.position;
    }

    public override void OnTick()
    {
        
    }

    public override void SetPosition(Vector2 pos)
    {
        transform.position = pos;
    }

    public override void Setup(params object[] objects)
    {
        if (objects.Length > 0) startPos = (Vector2)objects[0];
        if (objects.Length > 1) direction = (Vector2)objects[1];
        if (objects.Length > 2) speed = (float)objects[2];
    }
}
