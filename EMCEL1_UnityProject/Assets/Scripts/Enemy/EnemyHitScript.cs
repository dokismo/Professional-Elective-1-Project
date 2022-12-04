using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Enemy.Animation;
using Core;
public class EnemyHitScript : MonoBehaviour, ITarget
{
    Transform MainEnemyTransform;
    EnemyHpHandler ThisEnemyHPScript;

    public Coroutine delayReset;

    public float DmgMultiplier = 1.5f;

    public bool isInRangeForZ5Effect = false;

    public float DmgReductionMultiplier = 1f;

    public bool isBoss;
    
    void Start()
    {
        delayReset = StartCoroutine(ResetWithDelay());

        if (isBoss)
            MainEnemyTransform = transform.parent.parent.parent.transform;
        else
            MainEnemyTransform = transform.parent.parent.transform;

        ThisEnemyHPScript = MainEnemyTransform.GetComponent<EnemyHpHandler>();
    }

    private void LateUpdate()
    {
        delayReset = StartCoroutine(ResetWithDelay());
    }
    public void Hit(int dmg)
    {
        float TotalDmg = (dmg * DmgMultiplier) * DmgReductionMultiplier;
        ThisEnemyHPScript.enemyHp -= TotalDmg;
        MainEnemyTransform.GetComponentInChildren<ChangeSpriteColorOnHit>().ApplyEffect();
        Debug.Log("ENEMY TOOK DAMAGE OF    " + TotalDmg);
        ThisEnemyHPScript.checkHealth();
    }

    public void StoppingTheCoroutine()
    {
        StopAllCoroutines();
    }
    public IEnumerator ResetWithDelay()
    {
        yield return new WaitForSeconds(1f);
        isInRangeForZ5Effect = false;
        DmgReductionMultiplier = 1f;
    }

   
}
