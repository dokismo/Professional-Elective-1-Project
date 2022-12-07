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
        if (GetComponent<EnemyHpHandler>().enemyHp <= HalfHP &&  !GetComponentInChildren<EnemyHitScript>().isSlowed)
        {
            
            ApplyZombie4Effect();
        }
    }


    void ApplyZombie4Effect()
    {
        GetComponentInChildren<Animator>().speed = 2;
        GetComponent<EnemyNavMeshScript>().enemyDamage = (GetComponent<EnemyApplyStats>().FinalDmg * 1.5f);
        GetComponent<NavMeshAgent>().speed = (GetComponent<EnemyApplyStats>().FinalSpeed * 3);
        EffectsApplied = true;
    }
}
