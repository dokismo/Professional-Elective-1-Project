using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
public class EnemyApplyStats : MonoBehaviour
{
    public ZombieStats EnemyStats;

    WaveDifficultyIncrement WaveDifficultyManager;

    NavMeshAgent EnemyNMA;
    EnemyNavMeshScript EnemyMainScript;
    EnemyHpHandler EnemyHealthManager;

    float Health, Damage, Speed;
    float HealthMultiplier, DmgMultiplier, SpeedMultiplier;

    private void Awake()
    {
        WaveDifficultyManager = GameObject.Find("For Wave Management").GetComponent<WaveDifficultyIncrement>();

        EnemyNMA = GetComponent<NavMeshAgent>();
        EnemyHealthManager = GetComponent<EnemyHpHandler>();
        EnemyMainScript = GetComponent<EnemyNavMeshScript>();

        GetStats();
        ApplyStats();
    }
   

    void GetStats()
    {
        Health = EnemyStats.Health;
        Damage = EnemyStats.Damage;
        Speed = EnemyStats.Speed;

        HealthMultiplier = EnemyStats.HealthMultiplier;
        DmgMultiplier = EnemyStats.DamageMultiplier;
        SpeedMultiplier = EnemyStats.SpeedMultiplier;

    }
    void ApplyStats()
    {
        EnemyHealthManager.enemyHp = Health * WaveDifficultyManager.HPDifficultyMultiplier;
        EnemyMainScript.enemyDamage = Damage * WaveDifficultyManager.DmgDifficultyMultiplier;

        EnemyNMA.speed = Speed * SpeedMultiplier;
        EnemyHealthManager.enemyHp = EnemyHealthManager.enemyHp * HealthMultiplier;
        EnemyMainScript.enemyDamage = EnemyMainScript.enemyDamage * DmgMultiplier;
    }
}
