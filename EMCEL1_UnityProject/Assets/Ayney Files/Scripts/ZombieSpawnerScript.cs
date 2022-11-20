using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ZombieSpawnerScript : MonoBehaviour
{
    public float defaultTimeToSpawn = 2f;
    float timeToSpawn;

    public ForSpawningScript forSpawnScript;

    public GameObject[] zombieGameObjects;

    public bool canSpawn = true;
   

    void Start()
    {
        transform.SetParent(GameObject.Find("SPAWNERS").transform);
        forSpawnScript = GameObject.Find("For Spawning").GetComponent<ForSpawningScript>();
        timeToSpawn = defaultTimeToSpawn;
    }

    void Update()
    {
        if(canSpawn)
        {
            SpawnZombie(Random.Range(0,2));
        }
    }


    void SpawnZombie(int Random)
    {
        if (timeToSpawn > 0f)
        {
            timeToSpawn -= Time.deltaTime;
        }
        else
        {
            if (forSpawnScript.zombiesSpawnedCount >= forSpawnScript.maxZombiesSpawned)
            {
                canSpawn = false;
                gameObject.SetActive(false);
                return;
            }
            Instantiate(zombieGameObjects[Random], transform.position, Quaternion.identity);
            timeToSpawn = defaultTimeToSpawn;
        }
    }
}
