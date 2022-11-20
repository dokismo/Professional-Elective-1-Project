using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ForSpawningScript : MonoBehaviour
{
    public delegate void callableFunction();
    public callableFunction functionToCall;

    public int zombiesSpawnedCount, maxZombiesSpawned = 30;

    public float defaultWaitTime = 5;
    float waitTime;
    public GameObject EnemiesStorer;
    public GameObject[] Spawners;

    private void Start()
    {
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
            waitTime = defaultWaitTime;
            function();
        }
    }

    public void DeactivateSpawners()
    {
        Debug.Log("Round Over, SPAWNING ZOMBIES");
        for (int i = 0; i < Spawners.Length; i++)
        {
            Spawners[i].SetActive(false);
            Spawners[i].GetComponent<ZombieSpawnerScript>().canSpawn = false;
        }
    }
    public void SetSpawnersActive()
    {
        Debug.Log("Round Over, SPAWNING ZOMBIES");
        for (int i = 0; i < Spawners.Length; i++)
        {
            Spawners[i].SetActive(true);
            Spawners[i].GetComponent<ZombieSpawnerScript>().canSpawn = true;
        }
    }
    

    public void ZombieLimiter()
    {
        if (zombiesSpawnedCount > maxZombiesSpawned)
        {
            Destroy(EnemiesStorer.transform.GetChild(0).gameObject);
        }
    }
}
