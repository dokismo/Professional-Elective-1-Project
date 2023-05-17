using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class ForSpawningScript : MonoBehaviour
{
    public int zombiesSpawnedCount, maxZombiesSpawned = 30, deadZombie;

    public WaveDifficultyIncrement WaveDifficultyManager;

    public float defaultWaitTime = 5;
    float waitTime;
    public GameObject EnemiesStorer;
    public GameObject[] Spawners;


    public bool CanSpawnBossEnemy = false;
    public int NumberOfBossToSpawn = 1;
    public int BossesSpawned = 0;

    public bool CanSpawnZombie4and5 = false;

    private bool roundIsStarting;



    public int zombieToSpawnNumNEW = 0;

    private void OnEnable()
    {
        ZombieSpawnerScript.SpawnedZombie += AddZombie;
        EnemyHpHandler.OnTheDeath += DeadZombie;
    }

    private void OnDisable()
    {
        ZombieSpawnerScript.SpawnedZombie += AddZombie;
        EnemyHpHandler.OnTheDeath -= DeadZombie;
    }

    private void AddZombie()
    {
        zombiesSpawnedCount++;

        if (zombiesSpawnedCount == maxZombiesSpawned)
            DeactivateSpawners();
    }

    private void DeadZombie()
    {
        deadZombie++;

        if (deadZombie != maxZombiesSpawned || deadZombie != zombiesSpawnedCount) return;


        
        deadZombie = 0;
        zombiesSpawnedCount = 0;
        WaveDifficultyManager.RoundEnd();
        Reset();
    }

    private void Start()
    {
        WaveDifficultyManager = GameObject.Find("For Wave Management").GetComponent<WaveDifficultyIncrement>();

        waitTime = defaultWaitTime;
        Spawners = GameObject.FindGameObjectsWithTag("spawner");
        EnemiesStorer = GameObject.Find("Enemies");

        roundIsStarting = true;
        //Reset();
    }
    
    private void Update()
    {
        ZombieLimiter();
        StartWaitTimer();
    }

    public void Reset()
    {
        WaveDifficultyManager.RoundEnd();
        roundIsStarting = true;
    }

    public void StartWaitTimer()
    {
        if (!roundIsStarting) return;

        if (waitTime >= 0)
            waitTime -= Time.deltaTime;
        else
        {
            roundIsStarting = false;
            WaveDifficultyManager.RoundStart();
            waitTime = defaultWaitTime;
            SetSpawnersActive();
        }
    }

    
    public void DeactivateSpawners()
    {
        for (int i = 0; i < Spawners.Length; i++)
        {
            spawnSfx();
            Spawners[i].SetActive(false);
            Spawners[i].GetComponent<ZombieSpawnerScript>().canSpawn = false;
            Spawners[i].GetComponent<NewZombieSpawnerScript>().canSpawn = false;
        }
    }
    public void SetSpawnersActive()
    {
        for (int i = 0; i < Spawners.Length; i++)
        {
            spawnSfx();
            Spawners[i].SetActive(true);
            Spawners[i].GetComponent<ZombieSpawnerScript>().canSpawn = true;
            Spawners[i].GetComponent<NewZombieSpawnerScript>().canSpawn = true;
        }
    }

    public void spawnSfx() //sfx
    {
        SFXManager sfxManager = FindObjectOfType<SFXManager>();//sfx

        if (sfxManager)
            sfxManager.Play("spawnZ");
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
