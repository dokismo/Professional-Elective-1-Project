using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyHpHandler : MonoBehaviour
{
    public float enemyHp = 100f;


    public void takeDamage(float dmg)
    {
        enemyHp -= dmg;
        checkHealth();
    }
    public void checkHealth()
    {
        if(enemyHp <= 0f)
        {
            Destroy(gameObject);
        }
    }
}
