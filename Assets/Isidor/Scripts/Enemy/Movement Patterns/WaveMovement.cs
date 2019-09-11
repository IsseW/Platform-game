using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaveMovement : EnemyMovement
{
    public bool horizontal;
    public Vector2 startPos;
    public float amplitude; // height of wave
    public float period; // length of a wave

    public override Vector2 GetPosition(float time)
    {
        return horizontal ? new Vector2(startPos.x + time * period, startPos.y + Mathf.Sin(time) * amplitude) : new Vector2(startPos.y + Mathf.Sin(time) * amplitude, startPos.x + time * period);
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
