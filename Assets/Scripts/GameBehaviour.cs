using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameBehaviour : MonoBehaviour
{
    [SerializeField] private GameObject prefabAsteroid;
    private float spawnTime = 3.0f;
    private float spawnDelay = 2.0f;
    private float spawnDistance = 12.0f;


    void Start()
    {
        //Will create an asteroid once in a while
        InvokeRepeating(nameof(SpawnAsteroid), spawnTime, spawnDelay);
    }

    void SpawnAsteroid()
    {
        //Spawns asteroids on the edge of the circle in a random position
        Vector2 spawnPosition = Random.insideUnitCircle.normalized * spawnDistance;
        Instantiate(prefabAsteroid, spawnPosition, Quaternion.identity);
    }
}
