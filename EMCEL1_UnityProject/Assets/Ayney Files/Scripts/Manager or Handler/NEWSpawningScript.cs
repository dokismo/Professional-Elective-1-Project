using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using System;
using UnityEngine.SceneManagement;

public class NEWSpawningScript : MonoBehaviour
{
    public static NEWSpawningScript Instance;


    public delegate void ZombieDeath();
    public static ZombieDeath zombieDeathDelegate;

    [SerializeField] List<Transform> SpawnLocations;
    [SerializeField] GameObject[] ZombieType;
    [SerializeField] float SpawnInterval = 1f;
    public int MaxZombieToSpawn = 5, ZombiesSpawned, ZombieDead;
    int DefaultMaxSpawn;
    public bool CanSpawn, IsEndDoorOpen = false;
    float DEFAULTSpawnInterval;


    private void Awake()
    {
        if (Instance != null) Destroy(gameObject);
        else
        {
            Instance = this;
            DontDestroyOnLoad(this);
        }

        DefaultMaxSpawn = MaxZombieToSpawn;
        DEFAULTSpawnInterval = SpawnInterval;
    }
    private void OnEnable()
    {
        BossFightManager.OnBossStart += BossStartFunc;
        PlayerUIHandler.onMainMenu += DestroyThis;
        SceneManager.sceneLoaded += SetUpSpawner;
        zombieDeathDelegate += AddDeadZombie;
    }
    private void OnDisable()
    {
        BossFightManager.OnBossStart -= BossStartFunc;
        PlayerUIHandler.onMainMenu -= DestroyThis;
        SceneManager.sceneLoaded -= SetUpSpawner;
        zombieDeathDelegate -= AddDeadZombie;
    }

    private void Start()
    {
        SpawnLocations = new List<Transform>(GameObject.Find("SPAWNERS").GetComponentsInChildren<Transform>());

        // Removes the parents transform that is wrongfully fetched by "GetComponentsInChildren".
        SpawnLocations.RemoveAt(0);

        ZombiesSpawned = 0;
        SpawnInterval = DEFAULTSpawnInterval;

        CanSpawn = true;
    }

    void Update()
    {
        if(CanSpawn && !IsEndDoorOpen) StartSpawning();
    }

    void StartSpawning()
    {
        // Timer for Interval.
        if (SpawnInterval > 0f) SpawnInterval -= Time.deltaTime;

        else
        {
            // Checks if zombie limit has been reached, if limit is reached then spawning will be disabled.
            if (ZombiesSpawned < MaxZombieToSpawn)
            {
                // Instantiates RANDOM zombie type at a RANDOM location, AND resets the timer to default value.
                Instantiate(ZombieType[UnityEngine.Random.Range(0, ZombieType.Length)], SpawnLocations[UnityEngine.Random.Range(0, SpawnLocations.Count)].position, Quaternion.identity);
                SpawnInterval = DEFAULTSpawnInterval;
                ZombiesSpawned++;
            }
            else CanSpawn = false;
        }
    }

    void AddDeadZombie()
    {
        ZombieDead++;
    }

    void SetUpSpawner(Scene sceneName, LoadSceneMode mode)
    {
        // Fetches the list of transform of the spawn location.
        SpawnLocations = new List<Transform>(GameObject.Find("SPAWNERS").GetComponentsInChildren<Transform>());

        // Removes the parents transform that is wrongfully fetched by "GetComponentsInChildren".
        SpawnLocations.RemoveAt(0);

        ZombiesSpawned = 0;
        MaxZombieToSpawn = DefaultMaxSpawn;
        SpawnInterval = DEFAULTSpawnInterval;
        
        CanSpawn = true;
    }

    public void BossStartFunc()
    {
        CanSpawn = false;
        DestroyAllZombiesOnScene();
    }

    void DestroyAllZombiesOnScene()
    {
        GameObject[] Zombies = GameObject.FindGameObjectsWithTag("Enemy");

        foreach(GameObject zombs in Zombies)
        {
            Destroy(zombs);
        }
    }

    public void DestroyThis()
    {
        Destroy(gameObject);
    }
}
