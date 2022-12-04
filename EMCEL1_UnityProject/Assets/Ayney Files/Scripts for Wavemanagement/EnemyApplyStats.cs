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
        EnemyNMA.speed = Speed * SpeedMultiplier;
        EnemyHealthManager.enemyHp = Health * HealthMultiplier;
        EnemyMainScript.enemyDamage = Damage * DmgMultiplier;

        EnemyHealthManager.enemyHp *= (WaveDifficultyManager.HPDifficultyMultiplier + 1);
        EnemyMainScript.enemyDamage *=   (WaveDifficultyManager.DmgDifficultyMultiplier + 1);
    }
}
