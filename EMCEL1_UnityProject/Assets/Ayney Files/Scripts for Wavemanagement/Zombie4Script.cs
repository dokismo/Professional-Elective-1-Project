using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Zombie4Script : MonoBehaviour
{

    public float HalfHP;
    public bool EffectsApplied = false;
    private void Start()
    {
        
        HalfHP = GetComponent<EnemyHpHandler>().enemyHp / 2;
    }
    void Update()
    {
        if (GetComponent<EnemyHpHandler>().enemyHp <= HalfHP && !EffectsApplied)
        {
            ApplyZombie4Effect();
        }
    }


    void ApplyZombie4Effect()
    {
        GetComponent<EnemyNavMeshScript>().enemyDamage *= 1.5f;
        GetComponent<NavMeshAgent>().speed *= 3f;
        EffectsApplied = true;
    }
}
