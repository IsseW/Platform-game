using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaveMovement : Movement
{
    public Vector2 direction;
    public Vector2 startPos;
    public float amplitude; // height of wave
    public float period; // length of a wave

    public override Vector2 GetPosition(float time)
    {
        direction = direction.normalized;
        return direction * time * period + Vector2.Perpendicular(direction) * Mathf.Sin(time) * amplitude + startPos;
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
        if (objects.Length > 2) amplitude = (float)objects[2];
        if (objects.Length > 3) period = (float)objects[3];
    }
}
