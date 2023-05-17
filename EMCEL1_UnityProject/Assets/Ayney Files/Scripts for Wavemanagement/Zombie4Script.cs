using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Zombie4Script : MonoBehaviour
{

    public float HalfHP;
    public bool EffectsApplied = false;
    
    private EnemyHpHandler enemyHpHandler;
    private EnemyHitScript enemyHitScript;
    private Animator animator;
    private EnemyNavMeshScript enemyNavMeshScript;
    private EnemyApplyStats enemyApplyStats;
    private NavMeshAgent navMeshAgent;
    private EnemyApplyStats enemyApplyStats1;


    private void Start()
    {
        enemyApplyStats1 = GetComponent<EnemyApplyStats>();
        navMeshAgent = GetComponent<NavMeshAgent>();
        enemyApplyStats = GetComponent<EnemyApplyStats>();
        enemyNavMeshScript = GetComponent<EnemyNavMeshScript>();
        animator = GetComponentInChildren<Animator>();
        enemyHitScript = GetComponentInChildren<EnemyHitScript>();
        enemyHpHandler = GetComponent<EnemyHpHandler>();

        HalfHP = GetComponent<EnemyHpHandler>().enemyHp / 2;
    }
    void Update()
    {
        if (enemyHpHandler.enemyHp <= HalfHP &&  !enemyHitScript.isSlowed)
        {
            
            ApplyZombie4Effect();
        }
    }


    void ApplyZombie4Effect()
    {
        animator.speed = 2;
        enemyNavMeshScript.enemyDamage = (enemyApplyStats.FinalDmg * 1.5f);
        navMeshAgent.speed = (enemyApplyStats1.FinalSpeed * 3);
        EffectsApplied = true;
    }
}
