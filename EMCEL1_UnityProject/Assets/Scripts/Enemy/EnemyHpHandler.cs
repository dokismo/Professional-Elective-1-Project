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
    bool isAlive = true;
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
            if (isAlive)
            {
                int MoneyDropped = Random.Range(MinimumMoney, MaxMoney);
                DropMoney(MoneyDropped); 
            }
            isAlive = false;
        }
    }


    void DropMoney(int money)
    {
        PlayerStatus.getMoney(money);
    }
}
