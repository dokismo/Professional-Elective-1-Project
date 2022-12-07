using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Zombie5AbilityScript : MonoBehaviour
{
    public GameObject Minion;

    public LayerMask TargetLayersForZ5;

    public float DefaultSpawnRate = 2;
    public float MinionSpawnRadius = 2f;

    float SpawnrateTimer;
    int MinionsPerSpawn = 5;

    private void Start()
    {
        SpawnrateTimer = DefaultSpawnRate;
    }

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
        // Had to do this because of fucking error "failed to create agent because it is not close enough to the navmesh"
        for (int i = 0; i < MinionsPerSpawn; i++)
        {
            RaycastHit hit;
            Ray directionOfRay = new Ray(Random.insideUnitSphere * MinionSpawnRadius + transform.localPosition + Vector3.up, -Vector2.up);
            if (Physics.Raycast(directionOfRay, out hit,100f, TargetLayersForZ5, QueryTriggerInteraction.Ignore) || hit.point == null)
            {
                GameObject MinionChild = Instantiate(Minion, hit.point, Quaternion.identity);
                MinionChild.transform.SetParent(transform);
            }
            else
            {
                directionOfRay = new Ray(Random.insideUnitSphere * MinionSpawnRadius + transform.localPosition + Vector3.up * 10f, -Vector2.up);
                Physics.Raycast(directionOfRay, out hit, 100f);
            }
        }
    }
}
