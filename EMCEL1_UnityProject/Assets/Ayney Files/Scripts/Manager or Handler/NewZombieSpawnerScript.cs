using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class NewZombieSpawnerScript : MonoBehaviour
{
    public static event Action SpawnedZombie;

    public float defaultTimeToSpawn = 2f;
    float timeToSpawn;

    public ForSpawningScript forSpawnScript;
    public WaveDifficultyIncrement WaveDifficultyManager;

    public GameObject[] zombieGameObjects;

    public GameObject[] BossZombies;

    public bool canSpawn = true;

    public int ZombieToSpawnNum = 0;

    void Start()
    {
        transform.SetParent(GameObject.Find("SPAWNERS").transform);
        forSpawnScript = GameObject.Find("For Spawning").GetComponent<ForSpawningScript>();
        timeToSpawn = defaultTimeToSpawn;

        WaveDifficultyManager = GameObject.Find("For Wave Management").GetComponent<WaveDifficultyIncrement>();
    }

    void Update()
    {
        if (canSpawn)
        {
            switch (forSpawnScript.zombieToSpawnNumNEW)
            {
                case 0:
                    SpawnZombie(0);
                    break;
                case 1:
                    SpawnZombie(1);
                    break;
                case 2:
                    SpawnZombie(2);
                    break;
            }
        }
    }

    void SpawnZombie(int SpawnZombieNum)
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


                



                Transform[] Spawners = GetComponentsInChildren<Transform>();
                foreach(Transform obj in Spawners)
                {
                    obj.gameObject.SetActive(false);
                }
                return;
            }
            Spawn(zombieGameObjects[SpawnZombieNum]);

            
            timeToSpawn = defaultTimeToSpawn;
        }
    }

    private void Spawn(GameObject objToSpawn)
    {
        forSpawnScript.zombiesSpawnedCount++;
        Instantiate(objToSpawn, transform.position, Quaternion.identity);
        SpawnedZombie?.Invoke();
    }


}
