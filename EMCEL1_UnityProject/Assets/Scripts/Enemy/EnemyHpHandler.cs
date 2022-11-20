using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Core;
using Enemy.Animation;

public class EnemyHpHandler : MonoBehaviour, ITarget
{
    public int enemyHp = 100;

    public void Hit(int dmg)
    {
        enemyHp -= dmg;
        checkHealth();
        transform.GetComponentInChildren<ChangeSpriteColorOnHit>().ApplyEffect();
    }
    public void checkHealth()
    {
        if(enemyHp <= 0f)
        {
            Destroy(gameObject);
        }
    }

    
}
