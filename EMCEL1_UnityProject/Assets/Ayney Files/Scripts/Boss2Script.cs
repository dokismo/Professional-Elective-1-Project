using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using Player.Control;
public class Boss2Script : MonoBehaviour
{
    [SerializeField] Animator BossAnimator;

    public NavMeshAgent BossNavmesh;

    public Transform Player;
    [SerializeField] Transform SmashLocation;

    [SerializeField] GameObject SmashParticle;

    public bool IsRunning = false, IsDead = false, IsIdle = false, IsStunned = false, IsSmashing = false,
        IsAttacking = false, IsScreaming = false, PlayerInRange = false, AppliedEnrage = false;

    public float RandomAttackVar;

    [SerializeField] float Speed;
    float NormalAttackDmg = 20f;
    float SmashDmg = 40f;
    float SmashRadius = 4f;

    float EnragedMultiplier = 1.5f;
    void Start()
    {
        BossAnimator.speed = 1.2f;
        BossNavmesh.speed = Speed;
        BossFightManager.Instance?.AssignBossVar();
    }

    // Update is called once per frame
    void Update()
    {
        if (BossFightManager.Instance.BossFightStarted)
        {
            if (!BossNavmesh.isStopped && !IsIdle && !IsScreaming && !IsAttacking && !IsDead)
            {
                IsRunning = true;
                BossNavmesh.destination = Player.position;
            }

            if (IsStunned) BossNavmesh.isStopped = true;

            if(IsAttacking)
            {
                transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.LookRotation(Player.transform.position - transform.position), Time.deltaTime * 1.5f);
                BossNavmesh.isStopped = true;
            }

            if(IsDead)
            {
                BossNavmesh.isStopped = true;
                IsRunning = false;
                IsIdle = false;
                IsStunned = false;
                IsSmashing = false;
                IsAttacking = false;
                IsScreaming = false;
                PlayerInRange = false;
            }
        }

        
        AnimationSetter();
    }

    void AnimationSetter()
    {
        if (!BossAnimator.GetBool("IsDead"))
        {
            BossAnimator.SetBool("IsSmashing", IsSmashing);
            BossAnimator.SetBool("IsRunning", IsRunning);
            BossAnimator.SetBool("IsIdle", IsIdle);
            BossAnimator.SetBool("IsAttacking", IsAttacking);
            BossAnimator.SetBool("IsScreaming", IsScreaming);
            BossAnimator.SetBool("IsDead", IsDead);
        }
    }

    public void Scream()
    {
        BossNavmesh.isStopped = true;
        IsSmashing = false;
        IsRunning = false;
        IsIdle = false;
        IsAttacking = false;
        IsScreaming = true;
    }
    public void StopScream()
    {
        BossNavmesh.isStopped = false;
        IsSmashing = false;
        IsRunning = true;
        IsIdle = false;
        IsAttacking = false;
        IsScreaming = false;
    }

    public void SmashAbility()
    {
        StartCoroutine(CameraEffectsHandler.Instance?.CameraShake(1, 1));
        GameObject obj = Instantiate(SmashParticle, SmashLocation.position, SmashParticle.transform.rotation);
        obj.GetComponent<SphereCollider>().radius = SmashRadius;
        obj.GetComponent<SmashDetect>().SmashDmg = SmashDmg;
    }

    public void AttackPlayer()
    {
        if(PlayerInRange)
        {
            StartCoroutine(CameraEffectsHandler.Instance?.CameraShake(1, 2));
            PlayerStatus.changeHealth?.Invoke(-(int)NormalAttackDmg);
        }
    }

    public void StopAttack()
    {
        
        IsAttacking = false;
        IsSmashing = false;
        if(!PlayerInRange)
        {
            IsRunning = true;
            BossNavmesh.isStopped = false;
        }
            
        RandomAttackSelector();
    }

    public void RandomAttackSelector()
    {
        RandomAttackVar = Random.Range(0, 2);
    }

    public void ApplyEnragedStats()
    {
        if(GetComponent<BossHPHandler>().HasRaged && !AppliedEnrage)
        {
            NormalAttackDmg = NormalAttackDmg * EnragedMultiplier;
            SmashDmg = SmashDmg * EnragedMultiplier;
            SmashRadius = SmashRadius * EnragedMultiplier;
            AppliedEnrage = true;
            BossNavmesh.speed = Speed * EnragedMultiplier;
            BossAnimator.speed = 1.7f;
            TransformEnrageMode();
        }
    }

    public void ScreamEffects()
    {
        StartCoroutine(CameraEffectsHandler.Instance?.CameraShake(2f, 0.5f));
    }

    public void TransformEnrageMode()
    {
        StartCoroutine(StartTransformToEnrage());
    }


    IEnumerator StartTransformToEnrage()
    {
        float TransformDuration = 2f;
        float TimeElapsed = 0f;

        Vector3 EnragedScale = new Vector3(2f, 2f, 2f);

        while(TimeElapsed < TransformDuration)
        {
            TimeElapsed = Time.deltaTime;
            transform.localScale = Vector3.Lerp(transform.localScale, EnragedScale, TimeElapsed / TransformDuration);
            yield return null;
        }
        yield break;
    }

    public void Death()
    {
        NextSceneNameHolder.Instance.NextSceneName = "Exit";
        SceneLoader.Instance.NextScene();
    }
    
    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
        {
            RandomAttackSelector();
        }
    }
    private void OnTriggerStay(Collider other)
    {
        if (other.tag == "Player")
        {
            PlayerInRange = true;
            if(RandomAttackVar == 1)
            {
                BossNavmesh.isStopped = true;
                IsRunning = false;
                IsAttacking = true;
            } else if (RandomAttackVar == 0)
            {
                BossNavmesh.isStopped = true;
                IsRunning = false;
                IsSmashing = true;
            }
            
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if(other.tag == "Player")
        {
            PlayerInRange = false;
        }
    }
}
