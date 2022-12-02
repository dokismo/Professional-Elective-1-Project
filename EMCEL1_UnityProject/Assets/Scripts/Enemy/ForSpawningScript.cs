using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ForSpawningScript : MonoBehaviour
{
    public delegate void callableFunction();
    public callableFunction functionToCall;

    public int zombiesSpawnedCount, maxZombiesSpawned = 30;

    WaveDifficultyIncrement WaveDifficultyManager;

    public float defaultWaitTime = 5;
    float waitTime;
    public GameObject EnemiesStorer;
    public GameObject[] Spawners;


    public bool CanSpawnBossEnemy = false;
    public int NumberOfBossToSpawn = 1;
    public int BossesSpawned = 0;
    private void Start()
    {
        WaveDifficultyManager = GameObject.Find("For Wave Management").GetComponent<WaveDifficultyIncrement>();

        waitTime = defaultWaitTime;
        Spawners = GameObject.FindGameObjectsWithTag("spawner");
        DeactivateSpawners();
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
            WaveDifficultyManager.RoundEnd();
            functionToCall = SetSpawnersActive;
            StartWaitTimer(functionToCall);
        }
        ZombieLimiter();
        
    }

    public void StartWaitTimer(callableFunction function)
    {
        if (waitTime >= 0)
        {
            waitTime -= Time.deltaTime;
        }
        else
        {
            WaveDifficultyManager.RoundStart();
            waitTime = defaultWaitTime;
            function();
        }
    }

    
    public void DeactivateSpawners()
    {
        
        Debug.Log("Round Over, SPAWNING ZOMBIES");
        for (int i = 0; i < Spawners.Length; i++)
        {
            spawnSfx();
            Spawners[i].SetActive(false);
            Spawners[i].GetComponent<ZombieSpawnerScript>().canSpawn = false;
        }

    }
    public void SetSpawnersActive()
    {
        Debug.Log("Round Over, SPAWNING ZOMBIES");
        for (int i = 0; i < Spawners.Length; i++)
        {
            spawnSfx();
            Spawners[i].SetActive(true);
            Spawners[i].GetComponent<ZombieSpawnerScript>().canSpawn = true;
           
        }
    }

    public void spawnSfx() //sfx
    {
        FindObjectOfType<SFXManager>().Play("spawnZ");//sfx
        Debug.Log("SPAWN SFX");
    }


    public void ZombieLimiter()
    {
        if (zombiesSpawnedCount > maxZombiesSpawned)
        {
            int RandomDestroy = Random.Range(0, EnemiesStorer.transform.childCount);
            if(!EnemiesStorer.transform.GetChild(RandomDestroy).transform.name.Contains("Boss"))
            {
                Destroy(EnemiesStorer.transform.GetChild(RandomDestroy).gameObject);
            }
            else
            {
                ZombieLimiter();
            }
        }
    }
}
