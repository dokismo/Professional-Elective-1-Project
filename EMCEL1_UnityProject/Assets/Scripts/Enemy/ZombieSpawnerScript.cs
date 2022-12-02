using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ZombieSpawnerScript : MonoBehaviour
{
    public float defaultTimeToSpawn = 2f;
    float timeToSpawn;

    public ForSpawningScript forSpawnScript;
    public WaveDifficultyIncrement WaveDifficultyManager;

    public GameObject[] zombieGameObjects;

    public GameObject[] BossZombies;

    public bool canSpawn = true;



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
            SpawnZombie(Random.Range(0, 10));
        }
        
    }

    void SpawnZombie(int RandomNum)
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

            //Spawn Boss 
            if(forSpawnScript.CanSpawnBossEnemy && forSpawnScript.zombiesSpawnedCount >= forSpawnScript.maxZombiesSpawned / 2 && forSpawnScript.BossesSpawned < forSpawnScript.NumberOfBossToSpawn)
            {
                 Instantiate(BossZombies[0], transform.position, Quaternion.identity);
                 forSpawnScript.BossesSpawned++;
            } else if (forSpawnScript.BossesSpawned >= forSpawnScript.NumberOfBossToSpawn)
            {
                forSpawnScript.CanSpawnBossEnemy = false;
                forSpawnScript.BossesSpawned = 0;
            }


            // Spawn Zombies
            if(RandomNum >= 0 && RandomNum<= 6)
            {
                int RandomPicker = Random.Range(0,2);
                Instantiate(zombieGameObjects[RandomPicker], transform.position, Quaternion.identity);
            } else if (RandomNum >= 7 && RandomNum <=8)
            {
                Instantiate(zombieGameObjects[2], transform.position, Quaternion.identity);
            } else if (RandomNum == 9)
            {
                if (WaveDifficultyManager.WaveNumber >= 6 && WaveDifficultyManager.WaveNumber < 10)
                {
                    Instantiate(zombieGameObjects[3], transform.position, Quaternion.identity);
                } else if (WaveDifficultyManager.WaveNumber >= 10)
                {
                    int RandomNum2 = Random.Range(3, 5);
                    Instantiate(zombieGameObjects[RandomNum2], transform.position, Quaternion.identity);
                }
                
            }

            timeToSpawn = defaultTimeToSpawn;
        }

        

    }

    
}
