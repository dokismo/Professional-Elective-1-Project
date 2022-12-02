using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Enemy.Animation;
using Core;
public class EnemyHitScript : MonoBehaviour, ITarget
{
    Transform MainEnemyTransform;
    EnemyHpHandler ThisEnemyHPScript;

    public float DmgMultiplier = 1.5f;
    void Start()
    {
        MainEnemyTransform = transform.parent.parent.transform;
        ThisEnemyHPScript = MainEnemyTransform.GetComponent<EnemyHpHandler>();
    }

    public void Hit(int dmg)
    {
        float TotalDmg = dmg * DmgMultiplier;
        ThisEnemyHPScript.enemyHp -= TotalDmg;
        MainEnemyTransform.GetComponentInChildren<ChangeSpriteColorOnHit>().ApplyEffect();
        Debug.Log("ENEMY TOOK DAMAGE OF    " + TotalDmg);
        ThisEnemyHPScript.checkHealth();
    }

   
}
