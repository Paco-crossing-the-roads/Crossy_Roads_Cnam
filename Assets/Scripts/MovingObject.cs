using Assets.Scripts;
using UnityEngine;

public abstract class MovingObject : MonoBehaviour
{
    [SerializeField] public float speed;

    public float limiteZPositive = 25f;
    public float limiteZNegative = -25f;

    private void Update()
    {
        if (!PauseManager.IsPaused)
        {
            Move();
            DestroyIfOutOfBound();
        }
    }
    protected abstract void Move();
    private void DestroyIfOutOfBound()
    {
        if (transform.position.z > limiteZPositive || transform.position.z < limiteZNegative)
        {
            Destroy(gameObject);
        }
    }
}
