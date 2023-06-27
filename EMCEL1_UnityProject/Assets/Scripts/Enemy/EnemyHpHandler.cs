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
    [SerializeField] bool IsRagdoll, InPracticeRange;
    [SerializeField] Animator Animator;
    [SerializeField] Rigidbody RagdollRoot;

    [SerializeField] Material[] ObjMat;
    [SerializeField] float TimeToDissolve;
    [SerializeField] GameObject DeathParticles;

    private void Awake()
    {
        ObjMat = transform.Find("Ragdoll").GetComponentInChildren<Renderer>().materials;
    }
    public void checkHealth()
    {
        if (!(enemyHp <= 0f) && !InPracticeRange) return;

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
        DeathParticles.SetActive(true);
        TurnOnRagdollMode();
        GetComponent<EnemyNavMeshScript>().EnemyNMAgent.enabled = false;
        if (!IsRagdoll)
        {
            GetComponent<EnemyNavMeshScript>().ZombieAnimatorController.Play("zombie_death_standing");
            GetComponent<EnemyNavMeshScript>().ZombieAnimatorController.SetBool("IsDead", true);
            DisableAllCollidersOnObject();
        }
        transform.Find("Attack Range").gameObject.SetActive(false);
        StartCoroutine(Dissolve());
        GlobalSfx.death?.Invoke(transform.position, deathSound);
        yield return new WaitForSeconds(TimeToDissolve + 2f);

        if (deathSound != null)
            
            Destroy(gameObject);

    }

    void TurnOnRagdollMode()
    {
        if (IsRagdoll && RagdollRoot != null)
        {
            RagdollMotion[] scripts = GetComponentsInChildren<RagdollMotion>();
            foreach (RagdollMotion script in scripts)
            {
                Animator.enabled = false;
                script.Dead();
            }
            RagdollRoot.isKinematic = false;
        }
    }
    void DisableAllCollidersOnObject()
    {
        Collider[] colliders = GetComponentsInChildren<Collider>();

        foreach(Collider col in colliders)
        {
            col.enabled = false;
        }
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

    IEnumerator Dissolve()
    {
        float progress = 0f;
        float time = Time.deltaTime;
        while(progress < 1f)
        {
            foreach(Material mat in ObjMat)
            {
                mat.SetFloat("_Dissolve", Mathf.Lerp(mat.GetFloat("_Dissolve"), 1f, time / TimeToDissolve));
            }
            yield return null;
        }
        yield break;
    }
}
