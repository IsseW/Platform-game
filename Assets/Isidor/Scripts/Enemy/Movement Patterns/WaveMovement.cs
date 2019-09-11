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
        return horizontal ? new Vector2() : Vector2.zero;
    }

    public override Vector2 GetPosition()
    {
        throw new System.NotImplementedException();
    }

    public override void OnTick()
    {
        throw new System.NotImplementedException();
    }

    public override void SetPosition(Vector2 pos)
    {
        throw new System.NotImplementedException();
    }
}
