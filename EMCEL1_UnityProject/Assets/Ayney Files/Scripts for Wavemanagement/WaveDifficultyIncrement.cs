using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaveDifficultyIncrement : MonoBehaviour
{
    ForSpawningScript SpawningScript;

    public int WaveNumber = 0;

    public float HPDifficultyMultiplier, DmgDifficultyMultiplier;

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
        if(WaveNumber % 5 == 0 && WaveNumber > 0)
        {
            SpawningScript.CanSpawnBossEnemy = true;
        }
        // Increase Number of bosses every 15 rounds
        SpawningScript.NumberOfBossToSpawn = 1 * (int)(WaveNumber / 15);
        if(SpawningScript.NumberOfBossToSpawn <= 0)
        {
            SpawningScript.NumberOfBossToSpawn = 1;
        }
        // Increase Enemy's health by 50% every 10 rounds
        HPDifficultyMultiplier = 0.5f * (int)(WaveNumber / 10);


        SpawningScript.maxZombiesSpawned += WaveNumber * 2;
        Debug.Log("DIFFICULTY INCREASED");
    }
}
