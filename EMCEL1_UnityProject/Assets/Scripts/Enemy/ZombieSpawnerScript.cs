using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class ZombieSpawnerScript : MonoBehaviour
{
    public static event Action SpawnedZombie;
    
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
            SpawnZombie(Random.Range(1, 11));
        }
    }

    void SpawnZombie(float RandomNum)
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
                 Spawn(BossZombies[0]);
                 forSpawnScript.BossesSpawned++;
            } else if (forSpawnScript.BossesSpawned >= forSpawnScript.NumberOfBossToSpawn)
            {
                forSpawnScript.CanSpawnBossEnemy = false;
                forSpawnScript.BossesSpawned = 0;
            }

            // Spawn Zombies

            if (WaveDifficultyManager.advancedEnemyChance.currentChanceSpawn >= RandomNum)
            {
                int RandomNum2 = Random.Range(3, 5);
                Spawn(zombieGameObjects[RandomNum2]);
            }
            else
            {
                float chance = Mathf.InverseLerp(
                    WaveDifficultyManager.advancedEnemyChance.currentChanceSpawn + 1,
                    10,
                    RandomNum);

                if (chance <= 0.7f)
                {
                    int RandomPicker = Random.Range(0,2);
                    Spawn(zombieGameObjects[RandomPicker]);
                }
                else
                {
                    Spawn(zombieGameObjects[2]);
                }
            }
            
            timeToSpawn = defaultTimeToSpawn;
        }

        

    }

    private void Spawn(GameObject objToSpawn)
    {
        Instantiate(objToSpawn, transform.position, Quaternion.identity);
        SpawnedZombie?.Invoke();
    }

    
}
