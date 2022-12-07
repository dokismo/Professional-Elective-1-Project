using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Enemy.Animation;
using Core;
using UnityEngine.AI;
public class EnemyHitScript : MonoBehaviour, ITarget
{
    Transform MainEnemyTransform;
    EnemyHpHandler ThisEnemyHPScript;

    NavMeshAgent EnemyNavmeshAgent;

    public Coroutine delayReset;

    public float DmgMultiplier = 1.5f;

    public bool isInRangeForZ5Effect = false;

    public float DmgReductionMultiplier = 1f;

    public bool isBoss, isSlowed = false;

    float DefaultSpeed;
    public float SlowDownTime = 0.3f, SlowEffectMultiplier = 0.3f;
    public float SlowedSpeed;
    void Start()
    {

        if (isBoss)
            MainEnemyTransform = transform.parent.parent.parent.transform;
        else
            MainEnemyTransform = transform.parent.parent.transform;

        ThisEnemyHPScript = MainEnemyTransform.GetComponent<EnemyHpHandler>();

        EnemyNavmeshAgent = (transform.parent.parent.GetComponent<NavMeshAgent>() != null) ? transform.parent.parent.GetComponent<NavMeshAgent>() : transform.parent.parent.parent.GetComponent<NavMeshAgent>();

        DefaultSpeed = (transform.parent.parent.GetComponent<EnemyApplyStats>() != null) ? transform.parent.parent.GetComponent<EnemyApplyStats>().FinalSpeed : transform.parent.parent.parent.GetComponent<EnemyApplyStats>().FinalSpeed;

        SlowedSpeed = DefaultSpeed * SlowEffectMultiplier;
    }

    private void Update()
    {
        if(isSlowed)
        {
            EnemyNavmeshAgent.speed = SlowedSpeed;
        }
    }
    public void Hit(int dmg)
    {
        StopAllCoroutines();
        StartCoroutine(SlowDown());
        float TotalDmg = (dmg * DmgMultiplier) * DmgReductionMultiplier;
        ThisEnemyHPScript.enemyHp -= TotalDmg;
        MainEnemyTransform.GetComponentInChildren<ChangeSpriteColorOnHit>().ApplyEffect();
        ThisEnemyHPScript.checkHealth();
    }

    
  

    IEnumerator SlowDown()
    {
        isSlowed = true;
        yield return new WaitForSeconds(SlowDownTime);
        isSlowed = false;
        EnemyNavmeshAgent.speed = DefaultSpeed;
    }

   
}
