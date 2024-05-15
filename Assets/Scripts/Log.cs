using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Log : MovingObject
{
    protected override void Move()
    {
        transform.Translate(Vector3.forward * speed * Time.deltaTime);
    }
}
