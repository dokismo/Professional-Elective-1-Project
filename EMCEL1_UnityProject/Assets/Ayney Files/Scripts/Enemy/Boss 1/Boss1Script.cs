using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Boss1Script : MonoBehaviour
{
    [SerializeField] Animator BossAnimator;

    

    public bool IsRunning = false, IsPreparing = false, IsCharging = false, 
        IsDead = false, IsIdle = false,IsStunned = false, PlayerInRange = false;

    float PrepareTime = 2f, ChargeAtTime = 2f, ChargingSpeed = 10f;

    public float ChargePower = 15f, ChargeDamage;

    [SerializeField] ParticleSystem RunParticle;

    [SerializeField] Collider ChargeCollider;

    public NavMeshAgent BossNavmesh;
    public Transform Player;
    void Start()
    {
        BossFightManager.Instance?.AssignBossVar();
    }

    // Update is called once per frame
    void Update()
    {
        if(BossFightManager.Instance.BossFightStarted)
        {
            if (!IsPreparing && !IsCharging && !BossNavmesh.isStopped && !IsIdle)
            {
                IsRunning = true;
                BossNavmesh.destination = Player.position;
            }
            if (IsPreparing) PrepareCharge();

            if (IsCharging)
            {
                transform.Translate(Vector3.forward * ChargingSpeed * Time.deltaTime);
                if (!RunParticle.isPlaying) RunParticle.Play();
            }
            else { if (RunParticle.isPlaying) RunParticle.Stop(); }


            if (IsStunned) BossNavmesh.isStopped = true;
        }
        AnimationSetter();
    }


    void PrepareCharge()
    {
        // Go backwards while rotating towards the player
        transform.LookAt(Player);
        transform.Translate(new Vector3(0, 0, 1 * -Time.deltaTime));
    }
    void AnimationSetter()
    {
        if (!BossAnimator.GetBool("IsDead"))
        {
            BossAnimator.SetBool("IsRunning", IsRunning);
            BossAnimator.SetBool("IsPreparing", IsPreparing);
            BossAnimator.SetBool("IsCharging", IsCharging);
        }
    }

    IEnumerator PrepareChargeEnum()
    {
        // Pause to make it look natural
        // Set to idle
        BossNavmesh.isStopped = true;
        IsPreparing = false;
        IsRunning = false;
        IsCharging = false;
        yield return new WaitForSeconds(0.5f);
        // set to preparing. setting to idle needed to make it look natural
        IsPreparing = true;
        IsRunning = false;
        IsCharging = false;
        yield return new WaitForSeconds(PrepareTime);
        StartCoroutine(Charge());

    }

    IEnumerator Charge()
    {
        IsPreparing = false;
        IsRunning = false;

        IsCharging = true;
        ChargeCollider.enabled = true;
        yield return new WaitForSeconds(ChargeAtTime);
        IsCharging = false;
        ChargeCollider.enabled = false;

        IsPreparing = false;
        IsRunning = true;
        BossNavmesh.isStopped = false;
    }

    IEnumerator SetIdle(float duration)
    {
        IsIdle = true;
        yield return new WaitForSeconds(duration);
        IsIdle = false;

    }
    public void StopCharge()
    {
        IsRunning = false;
        IsCharging = false;
        ChargeCollider.enabled = false;
        BossNavmesh.isStopped = false;
        StopAllCoroutines();
        StartCoroutine(SetIdle(0.5f));
    }

    

    private void OnTriggerStay(Collider other)
    {
        if (other.tag == "Player" && !IsStunned)
        {
            PlayerInRange = true;
            if (PlayerInRange && !IsCharging && !IsPreparing)
            {
                StartCoroutine(PrepareChargeEnum());
            }
            
        }


    }
    
    private void OnTriggerExit(Collider other)
    {
        if (other.tag == "Player") PlayerInRange = false;
    }

    
}
