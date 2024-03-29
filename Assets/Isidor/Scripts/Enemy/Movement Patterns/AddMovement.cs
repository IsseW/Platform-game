﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AddMovement : Movement
{
    public override void StartMovement()
    {
        base.StartMovement();
        if (movements != null)
        {
            for (int i = 0; i < movements.Length; i++)
            {
                movements[i].parent = parent;
            }
        }
    }

    public Movement[] movements;

    public override Vector2 GetPosition(float time)
    {
        Vector2 pos = Vector2.zero;
        for (int i = 0; i < movements.Length; i++)
        {
            pos += movements[i].GetPosition(time);
        }
        return pos;
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
        movements = (Movement[])objects[0];
    }
}
