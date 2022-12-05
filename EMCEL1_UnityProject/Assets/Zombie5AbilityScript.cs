using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Zombie5AbilityScript : MonoBehaviour
{
    public GameObject Minion;

    public float DefaultSpawnRate = 2;

    float SpawnrateTimer;
    int MinionsPerSpawn = 5;

    private void Start()
    {
        SpawnrateTimer = DefaultSpawnRate;
    }
    // Update is called once per frame
    void Update()
    {
        if(SpawnrateTimer > 0)
        {
            SpawnrateTimer -= Time.deltaTime;
        } else
        {
            SpawnMinion();
            SpawnrateTimer = DefaultSpawnRate;
        }
    }

    void SpawnMinion()
    {
        for(int i = 0; i < MinionsPerSpawn; i++)
        {
            GameObject MinionChild = Instantiate(Minion, Random.insideUnitSphere + transform.localPosition , Quaternion.identity);
            MinionChild.transform.SetParent(transform);
            MinionChild.transform.position = new Vector3(MinionChild.transform.position.x, transform.localPosition.y, MinionChild.transform.position.z);

        }
    }
}
