using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class forSpawningScript : MonoBehaviour
{
    public int zombiesSpawnedCount, maxZombiesSpawned = 30;
    float waitTime = 5, defaultWaitTime;
    public GameObject EnemiesStorer;
    public Transform[] spawnedZombies;
    public GameObject[] Spawners;

    private void Start()
    {
        Spawners = GameObject.FindGameObjectsWithTag("spawner");
        EnemiesStorer = GameObject.Find("Enemies");
    }
    private void Update()
    {
        checkZombieCount();
    }

    public void checkZombieCount()
    {
        zombiesSpawnedCount = EnemiesStorer.transform.childCount;
        if(zombiesSpawnedCount <= 0)
        {
            if (waitTime >= 0)
            {
                waitTime -= Time.deltaTime;
            }
            else
            {
                Debug.Log("Round Over, SPAWNING ZOMBIES");
                for (int i = 0; i < Spawners.Length; i++)
                {
                    Spawners[i].SetActive(true);
                    Spawners[i].GetComponent<zombieSpawnerScript>().canSpawn = true;
                }
                waitTime = defaultWaitTime;
            }
            
        }

        if(zombiesSpawnedCount > maxZombiesSpawned)
        {
            Destroy(EnemiesStorer.transform.GetChild(0).gameObject);
        }
    }
}
