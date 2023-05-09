using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using System;

public class NEWSpawningScript : MonoBehaviour
{
    public static NEWSpawningScript Instance;


    public delegate void ZombieDeath();
    public static ZombieDeath zombieDeathDelegate;

    [SerializeField] List<Transform> SpawnLocations;
    [SerializeField] GameObject[] ZombieType;
    [SerializeField] float SpawnInterval = 1f;
    public int MaxZombieToSpawn = 5, ZombiesSpawned, ZombieDead;
    public bool CanSpawn;
    float DEFAULTSpawnInterval;


    private void Awake()
    {
        if(Instance == null)
        {
            Instance = this;
        }

        DontDestroyOnLoad(gameObject);
    }
    private void OnEnable()
    {
        zombieDeathDelegate += AddDeadZombie;
    }
    private void OnDisable()
    {
        zombieDeathDelegate -= AddDeadZombie;
    }
    void Start()
    {
        CanSpawn = true;
        DEFAULTSpawnInterval = SpawnInterval;
        // Fetches the list of transform of the spawn location.
        SpawnLocations = new List<Transform>(GameObject.Find("SPAWNERS").GetComponentsInChildren<Transform>());

        // Removes the parents transform that is wrongfully fetched by "GetComponentsInChildren".
        SpawnLocations.RemoveAt(0);
    }

    void Update()
    {
        if(CanSpawn) StartSpawning();
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
}
