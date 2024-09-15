using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawner : MonoBehaviour
{

    public GameObject objectToSpawn;  // Assign the prefab in the Inspector
    public Vector3[] spawnPositions;  // Array to hold the different spawn positions
    public float spawnInterval = 2f;  // Time interval between spawns

    void Start()
    {
        // Start the spawning process
        InvokeRepeating("SpawnObject", 0f, spawnInterval);
    }

    void SpawnObject()
    {
        // Choose a random spawn position from the array
        int randomIndex = Random.Range(0, spawnPositions.Length);
        Vector3 spawnPosition = spawnPositions[randomIndex];

        // Instantiate the object at the chosen position with no rotation
        Instantiate(objectToSpawn, spawnPosition, Quaternion.identity);
    }
}
