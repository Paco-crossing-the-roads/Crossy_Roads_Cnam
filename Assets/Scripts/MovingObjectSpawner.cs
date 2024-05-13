using Assets.Scripts;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovingObjectSpawner : MonoBehaviour
{
    [SerializeField] private List<GameObject> spawnObject = new List<GameObject>();
    [SerializeField] private Transform spawnPos;
    [SerializeField] private float minSeparationTime;
    [SerializeField] private float maxSeparationTime;
    [SerializeField] private bool isRightSide;

    private void Start()
    {
        StartCoroutine(SpawnVehicle());
    }

    private IEnumerator SpawnVehicle()
    {
        while (true)
        {
            if (Time.timeScale != 0 && !PauseManager.IsPaused)
            {
                int whichObject = Random.Range(0, spawnObject.Count);
                yield return new WaitForSeconds(Random.Range(minSeparationTime, maxSeparationTime));
                GameObject go = Instantiate(spawnObject[whichObject], spawnPos.position, Quaternion.identity);
                if (!isRightSide)
                {
                    go.transform.Rotate(new Vector3(0, 180, 0));
                }
            }
            yield return null;
        }
    }
}
