using System;
using System.Collections;
using System.Collections.Generic;
using Audio_Scripts;
using UnityEngine;
using Core;
using Enemy.Animation;
using Player.Control;
using Random = UnityEngine.Random;

public class EnemyHpHandler : MonoBehaviour
{
    public static Action OnTheDeath;
    
    public float enemyHp = 100;

    public int MinimumMoney, MaxMoney;

    public GameObject deathSound;

    bool isAlive = true;
    
    public void checkHealth()
    {
        if (!(enemyHp <= 0f)) return;

        if (deathSound != null) 
            GlobalSfx.death?.Invoke(transform.position, deathSound);
        Destroy(gameObject);
        
        if (isAlive)
        {
            int MoneyDropped = Random.Range(MinimumMoney, MaxMoney);
            DropMoney(MoneyDropped);
        }
        
        isAlive = false;
    }

    private void OnDestroy()
    {
        OnTheDeath?.Invoke();
    }

    void DropMoney(int money)
    {
        PlayerStatus.getMoney(money);
    }
}
