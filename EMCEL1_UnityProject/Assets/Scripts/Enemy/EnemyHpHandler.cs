using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Core;
using Enemy.Animation;
using Player.Control;
public class EnemyHpHandler : MonoBehaviour
{
    public float enemyHp = 100;

    public int MinimumMoney, MaxMoney;

    
    public void checkHealth()
    {   
        if(enemyHp <= 0f)
        {
            FindObjectOfType<SFXManager>().Play("zombie_death");//sfx
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
