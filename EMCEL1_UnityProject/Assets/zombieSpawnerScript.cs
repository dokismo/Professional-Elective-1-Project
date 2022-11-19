using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class zombieSpawnerScript : MonoBehaviour
{
    public float defaultTimeToSpawn = 2f;

    float timeToSpawn, maxSpawnCount;

    public forSpawningScript forSpawnScript;

    public GameObject zombie;

    public bool canSpawn = true;
   

    void Start()
    {
        transform.SetParent(GameObject.Find("SPAWNERS").transform);
        forSpawnScript = GameObject.Find("For Spawning").GetComponent<forSpawningScript>();
        timeToSpawn = defaultTimeToSpawn;
    }

    // Update is called once per frame
    void Update()
    {
        if(canSpawn)
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
                Instantiate(zombie, transform.position, Quaternion.identity);
                timeToSpawn = defaultTimeToSpawn;
            }
        }
    }
}
