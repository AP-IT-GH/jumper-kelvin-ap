using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawner : MonoBehaviour
{
    public GameObject obstacle = null;
    public Transform obstacleSpawnPoint;
    public float spawnInterval = 1.5f;

    private float timeUntilSpawn;


    void Start()
    {
        timeUntilSpawn = spawnInterval;
    }

    void Update()
    {
        timeUntilSpawn -= Time.deltaTime;

        if (timeUntilSpawn <= 0)
        {
            SpawnPrefab();
            timeUntilSpawn = spawnInterval;
        }
    }

    void SpawnPrefab()
    {
        GameObject newObstacle = Instantiate(obstacle, obstacleSpawnPoint.position, obstacleSpawnPoint.rotation);
    }
}
