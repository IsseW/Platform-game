using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CircularMovement : EnemyMovement
{
    public Vector2 center;
    public Vector2 size;

    public override Vector2 GetPosition(float time)
    {
        return new Vector2(Mathf.Cos(time) * size.x + center.x, Mathf.Sin(time) * size.y + center.y);
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
