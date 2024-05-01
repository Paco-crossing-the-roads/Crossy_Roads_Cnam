using System.Collections;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;

public class TerrainGenerator : MonoBehaviour
{
    [SerializeField] private int minDistanceFromPlayer;
    [SerializeField] private int maxTerrainCount;
    [SerializeField] private List<TerrainData> terrainDatas = new List<TerrainData>();
    [SerializeField] private Transform terrainHolder;

    private List<GameObject> currentTerrains = new List<GameObject>();
    [HideInInspector] public Vector3 currentPosition = new Vector3(0, 0, 0);

    private void Start()
    {
        currentPosition.x -= 10;
        for (int i = 0; i < 4; i++)
        {
            InitialTerrain( new Vector3(0, 0, 0), 4);
        }
        for (int i = 4; i < 13; i++)
        {
            InitialTerrain( new Vector3(0, 0, 0), 3);
        }
        
        for (int i = 13; i < maxTerrainCount; i++)
        {
            SpawnTerrain(true, new Vector3(0, 0, 0));
        }

        maxTerrainCount = currentTerrains.Count;
    }
    
    public void InitialTerrain(Vector3 playerPos, int terrainTypeIndex)
    {
        GameObject terrain = Instantiate(terrainDatas[terrainTypeIndex].possibleTerrain[Random.Range(0, terrainDatas[terrainTypeIndex].possibleTerrain.Count)], currentPosition, Quaternion.identity, terrainHolder);
        currentTerrains.Add(terrain);
        currentPosition.x++;
    }
    
    public void SpawnTerrain(bool isStart, Vector3 playerPos, int terrainTypeIndex = -1)
    {
        if ((currentPosition.x - playerPos.x < minDistanceFromPlayer) || (isStart))
        {
            int whichTerrain = terrainTypeIndex >= 0 ? terrainTypeIndex : Random.Range(0, terrainDatas.Count);
            int terrainInSuccession = Random.Range(0, terrainDatas[whichTerrain].maxInSuccession);
            for (int i = 0; i < terrainInSuccession; i++)
            {
                GameObject terrain = Instantiate(terrainDatas[whichTerrain].possibleTerrain[Random.Range(0, terrainDatas[whichTerrain].possibleTerrain.Count)], currentPosition, Quaternion.identity, terrainHolder);
                currentTerrains.Add(terrain);
                currentPosition.x++;
            }
            if (!isStart)
            {
                if (currentTerrains.Count > maxTerrainCount)
                {
                    Destroy(currentTerrains[0]);
                    currentTerrains.RemoveAt(0);
                }
            }
        }
    }

}