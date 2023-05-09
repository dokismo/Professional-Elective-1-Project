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

        StartCoroutine(Death());
        
        if (isAlive)
        {
            int MoneyDropped = Random.Range(MinimumMoney, MaxMoney);
            DropMoney(MoneyDropped);
            EnemyDeath();
        }
        
        isAlive = false;
    }

    IEnumerator Death()
    {
        GetComponent<EnemyNavMeshScript>().ZombieAnimatorController.Play("zombie_death_standing");
        GetComponent<EnemyNavMeshScript>().ZombieAnimatorController.SetBool("IsDead", true);
        GetComponent<EnemyNavMeshScript>().EnemyNMAgent.enabled = false;
        transform.Find("Colliders").gameObject.SetActive(false);
        transform.Find("Attack Range").gameObject.SetActive(false);
        yield return new WaitForSeconds(3f);

        if (deathSound != null)
            GlobalSfx.death?.Invoke(transform.position, deathSound);
            Destroy(gameObject);

    }


    void EnemyDeath()
    {
        NEWSpawningScript.zombieDeathDelegate?.Invoke();
        OnTheDeath?.Invoke();
    }

    void DropMoney(int money)
    {
        PlayerStatus.getMoney(money);
    }
}
