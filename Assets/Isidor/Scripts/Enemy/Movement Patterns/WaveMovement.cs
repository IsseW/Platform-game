using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaveMovement : EnemyMovement
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
}
