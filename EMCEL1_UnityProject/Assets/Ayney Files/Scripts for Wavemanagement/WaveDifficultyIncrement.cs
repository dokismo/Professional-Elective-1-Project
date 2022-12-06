using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaveDifficultyIncrement : MonoBehaviour
{
    ForSpawningScript SpawningScript;

    public int WaveNumber = 0;

    [Header("       Increase Variable Every X Wave")]
    public int SpawnBossWAVE = 5;
    public int IncreaseBossWAVE = 15;

    public int IncreaseAllEnemyHealthWAVE = 10;

    public int ZombieIncrementEveryRound = 2;

    [HideInInspector]
    public float Z4And5BalancingNum = 0f;

    public float HPDifficultyMultiplier, DmgDifficultyMultiplier;

    [Header("       For amount of Increase Every Wave")]
    public float HPIncrementDifficulty = 0.5f;

    private void Awake()
    {
        SpawningScript = GameObject.Find("For Spawning").GetComponent<ForSpawningScript>();
    }

    public void RoundStart()
    {
        WaveNumber++;
        IncreaseDifficulty();
    }

    public void RoundEnd()
    {
        
    }


    void IncreaseDifficulty()
    {
        //Increment Zombie incremented every X round
        if(WaveNumber < 10)
        {
            ZombieIncrementEveryRound = 1;
        } else if(WaveNumber >= 11 && WaveNumber <= 20)
        {
            ZombieIncrementEveryRound = 2;
        } else if(WaveNumber >= 21)
        {
            ZombieIncrementEveryRound = 3;
        }

        // Spawn Zombie 4 and 5 at X Wave
        if(WaveNumber >=8 && WaveNumber <= 9)
        {
            Z4And5BalancingNum = 0;
            SpawningScript.CanSpawnZombie4and5 = true;
        } else if (WaveNumber >= 21 && WaveNumber <= 30)
        {
            Z4And5BalancingNum = 1;
            SpawningScript.CanSpawnZombie4and5 = true;
        } else if (WaveNumber >= 31)
        {
            Z4And5BalancingNum = 2;
            SpawningScript.CanSpawnZombie4and5 = true;
        }
        else
        {
            SpawningScript.CanSpawnZombie4and5 = false;
        }





        //
        if (WaveNumber % SpawnBossWAVE == 0 && WaveNumber > 0)
        {
            SpawningScript.CanSpawnBossEnemy = true;
        }
        // Increase Number of bosses every 15 rounds
        SpawningScript.NumberOfBossToSpawn = 1 * (int)(WaveNumber / IncreaseBossWAVE);
        if(SpawningScript.NumberOfBossToSpawn <= 0)
        {
            SpawningScript.NumberOfBossToSpawn = 1;
        }

        // Increase Enemy's health by 50% every 10 rounds
        HPDifficultyMultiplier = HPIncrementDifficulty * (int)(WaveNumber / IncreaseAllEnemyHealthWAVE);


        SpawningScript.maxZombiesSpawned += ZombieIncrementEveryRound;
        Debug.Log("DIFFICULTY INCREASED");
    }
}
