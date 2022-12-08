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
    public static event Action OnTheDeath;
    
    public float enemyHp = 100;

    public int MinimumMoney, MaxMoney;

    public AudioClip died;
    public AudioSource source;

    private OnEnemyInteract enemySound;
    bool isAlive = true;
    
    public void checkHealth()
    {   
        if(enemyHp <= 0f)
        {
            
            Destroy(gameObject);
            if (isAlive)
            {
                int MoneyDropped = Random.Range(MinimumMoney, MaxMoney);
                DropMoney(MoneyDropped);
            }
            isAlive = false;
        }
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
