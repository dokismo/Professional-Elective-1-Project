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

    bool IsGameManagerPresent = false;
    void Start()
    {
        if (GameObject.Find("Game Manager")) IsGameManagerPresent = true;
        else IsGameManagerPresent = false;


        if (isBoss)
            MainEnemyTransform = transform.parent.parent.parent.transform;
        else
            MainEnemyTransform = transform.parent.parent.transform;

        ThisEnemyHPScript = RootObject.GetComponent<EnemyHpHandler>();

        EnemyNavmeshAgent = (RootObject.GetComponent<NavMeshAgent>() != null) ? RootObject.GetComponent<NavMeshAgent>() : RootObject.GetComponent<NavMeshAgent>();

        DefaultSpeed = (RootObject.GetComponent<EnemyApplyStats>() != null) ? RootObject.GetComponent<EnemyApplyStats>().FinalSpeed : RootObject.GetComponent<EnemyApplyStats>().FinalSpeed;

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
        
        if(IsGameManagerPresent)
        {
            float TotalDmg = (dmg * DmgMultiplier) * DmgReductionMultiplier;
            
            ThisEnemyHPScript.enemyHp -= TotalDmg;
            // THIS LINE ONLY WORKS ON 2.5D ENEMIES ->  MainEnemyTransform.GetComponentInChildren<ChangeSpriteColorOnHit>().ApplyEffect();
            ThisEnemyHPScript.checkHealth();
        }
        
    }

    
  

    IEnumerator SlowDown()
    {
        isSlowed = true;
        yield return new WaitForSeconds(SlowDownTime);
        isSlowed = false;
        EnemyNavmeshAgent.speed = DefaultSpeed;
    }

   
}
