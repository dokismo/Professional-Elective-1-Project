using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

[Serializable]
public class EnemyChanceSpawn
{
    public int currentChanceSpawn = 0;
    public int difficulty1, difficulty2, difficulty3;
    

    public void SetChance(int waveNumber)
    {
        if (currentChanceSpawn == 0 && waveNumber >= difficulty1)
        {
            currentChanceSpawn++;
        }

        if (currentChanceSpawn == 1 && waveNumber >= difficulty2)
        {
            currentChanceSpawn++;
        }

        if (currentChanceSpawn == 2 && waveNumber >= difficulty3)
        {
            currentChanceSpawn++;
        }
    }
}

public class WaveDifficultyIncrement : MonoBehaviour
{
    public GameObject RoundNumAnim;
    
    ForSpawningScript SpawningScript;

    public int WaveNumber = 0;

    [Header("       Increase Variable Every X Wave")]
    public int SpawnBossWAVE = 5;
    public int IncreaseBossWAVE = 10;

    public int IncreaseAllEnemyHealthWAVE = 10;

    public int ZombieIncrementEveryRound = 2;

    public float HPDifficultyMultiplier;
    public float DmgDifficultyMultiplier;
    
    [Header("       For amount of Increase Every Wave")]
    public float HPIncrementDifficulty = 0.5f;

    public EnemyChanceSpawn advancedEnemyChance;

    private void Awake()
    {
        RoundNumAnim = GameObject.Find("RoundNumberAnim");
        SpawningScript = GameObject.Find("For Spawning").GetComponent<ForSpawningScript>();
    }

    private void Start()
    {
        SpawningScript.maxZombiesSpawned += WaveNumber;
    }

    public void RoundStart()
    {

        WaveNumber++;
        IncreaseDifficulty();
        StartWaveNumChange();
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
        if(WaveNumber >= 8 && WaveNumber <= 9)
        {
            SpawningScript.CanSpawnZombie4and5 = true;
        } else if (WaveNumber >= 21 && WaveNumber <= 30)
        {
            SpawningScript.CanSpawnZombie4and5 = true;
        } else if (WaveNumber >= 31)
        {
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
        
        advancedEnemyChance.SetChance(WaveNumber);
    }

    public void StartWaveNumChange()
    {
        RoundNumAnim.GetComponent<TextMeshProUGUI>().text = "WAVE " + WaveNumber;
        RoundNumAnim.GetComponent<Animator>().Play("Change Number",0,0);
    }
}
