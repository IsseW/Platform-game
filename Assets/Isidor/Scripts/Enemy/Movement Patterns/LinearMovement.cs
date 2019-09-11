using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LinearMovement : EnemyMovement
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
}
