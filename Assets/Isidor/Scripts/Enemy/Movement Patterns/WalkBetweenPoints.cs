﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WalkBetweenPoints : Movement
{
    public Vector2[] points; //Points to cycle through
    public float cycleLength; //Time of going from cycling through all points

    public override Vector2 GetPosition(float time)
    {
        float t = Mathf.PingPong(2 * points.Length * (time % cycleLength) / cycleLength, points.Length);
        int i = Mathf.FloorToInt(t);
        t -= i;
        return Vector2.Lerp(points[i], points[(i + 1) % points.Length], t);
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
        if (objects.Length > 0) points = (Vector2[])objects[0];
        if (objects.Length > 1) cycleLength = (float)objects[1];
    }


}