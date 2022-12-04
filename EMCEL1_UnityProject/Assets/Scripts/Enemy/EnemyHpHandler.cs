using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Core;
using Enemy.Animation;
using Player.Control;
using Random = UnityEngine.Random;

public class EnemyHpHandler : MonoBehaviour
{
    public float enemyHp = 100;

    public int MinimumMoney, MaxMoney;

    private SFXManager sfxManager;

    private void Start()
    {
        sfxManager = FindObjectOfType<SFXManager>();//sfx
    }

    public void checkHealth()
    {   
        if(enemyHp <= 0f)
        {
            if (sfxManager)
                sfxManager.Play("zombie_death"); //sfx
            Destroy(gameObject);
            DropMoney(Random.Range(MinimumMoney, MaxMoney));
            Debug.Log("ZOMBIE DIED SFX");
        }
    }


    void DropMoney(int money)
    {
        PlayerStatus.getMoney(money);
    }
}
