using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rotate : MonoBehaviour
{
    public Vector3 rotation;
    public float speed;

    void Update()
    {
        transform.rotation = Quaternion.Euler(transform.rotation.eulerAngles + rotation * speed);
    }
}
