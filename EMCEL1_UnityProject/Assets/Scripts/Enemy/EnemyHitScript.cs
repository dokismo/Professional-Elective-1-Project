using System.Collections;
using UnityEngine;
using Enemy.Animation;
using Core;
using UnityEngine.AI;
using UnityEditor;

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

    [SerializeField] GameObject RootObject;
    [SerializeField] EnemyHitEffect[] HitEffect;

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

        

        HitEffect = RootObject.GetComponentsInChildren<EnemyHitEffect>();
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
        for (int i = 0; i<HitEffect.Length; i++)
        {
            HitEffect[i].HitEffectOnMat();
        }
        
        float TotalDmg = (dmg * DmgMultiplier) * DmgReductionMultiplier;
        ThisEnemyHPScript.enemyHp -= TotalDmg;
        // THIS LINE ONLY WORKS ON 2.5D ENEMIES ->  MainEnemyTransform.GetComponentInChildren<ChangeSpriteColorOnHit>().ApplyEffect();
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
