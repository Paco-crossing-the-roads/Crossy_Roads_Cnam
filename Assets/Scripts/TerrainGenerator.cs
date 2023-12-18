using System.Collections;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;

public class TerrainGenerator : MonoBehaviour
{

    [SerializeField] private int maxTerrainCount;
    [SerializeField] private List<TerrainData> terrainDatas = new List<TerrainData>();
    [SerializeField] private Transform terrainHolder;

    private List<GameObject> currentTerrains = new List<GameObject>();
    private Vector3 currentPosition = new Vector3(0, 0, 0);

    private void Start()
    {
        for (int i = 0; i < maxTerrainCount; i++)
        {
            SpawnTerrain(true);
        }
        maxTerrainCount = currentTerrains.Count;
    }

    private void Update()
    {
        Thread.Sleep(1000);
        SpawnTerrain(false);
        /*
        if (Input.GetKeyDown(KeyCode.K))
        {
            SpawnTerrain(false);
        }
        */
    }

    private void SpawnTerrain(bool isStart)
    {
        int whichTerrain = Random.Range(0, terrainDatas.Count);
        int terrainInSuccession = Random.Range(0, terrainDatas[whichTerrain].maxInSuccession);
        for (int i = 0; i < terrainInSuccession; i++)
        {
            GameObject terrain = Instantiate(terrainDatas[whichTerrain].terrainPrefab, currentPosition, Quaternion.identity, terrainHolder);
            currentTerrains.Add(terrain);
            if (!isStart)
            {
                if (currentTerrains.Count > maxTerrainCount)
                {
                    Destroy(currentTerrains[0]);
                    currentTerrains.RemoveAt(0);
                }
            }
            currentPosition.x++;
        }
    }
}
