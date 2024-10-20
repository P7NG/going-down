using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotateObject : MonoBehaviour
{
    public float speed;

    private void FixedUpdate()
    {
        transform.Rotate(transform.forward, Time.deltaTime * speed);
    }
}
