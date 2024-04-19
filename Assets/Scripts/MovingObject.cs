using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovingObject : MonoBehaviour
{
    [SerializeField] private float speed;
    public bool isLog;

    public float limiteZPositive = 25f;
    public float limiteZNegative = -25f;

    private void Update()
    {
        transform.Translate(Vector3.forward * speed * Time.deltaTime);
        destroyIfOutOfBound();
    }
    private void destroyIfOutOfBound()
    {
        if (transform.position.z > limiteZPositive || transform.position.z < limiteZNegative)
        {
            Destroy(gameObject);
        }
    }
}
