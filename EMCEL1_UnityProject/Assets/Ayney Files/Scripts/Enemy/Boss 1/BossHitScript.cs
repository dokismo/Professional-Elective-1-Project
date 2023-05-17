using System.Collections;
using UnityEngine;
using Enemy.Animation;
using Core;
using UnityEngine.AI;

public class BossHitScript : MonoBehaviour, ITarget
{

    BossHPHandler BossHpHandlerScript;

    [SerializeField] float DamageMultiplier;
    void Start()
    {
        BossHpHandlerScript = transform.root.GetComponent<BossHPHandler>();
    }

    public void Hit(int damage)
    {
        BossHpHandlerScript.TakeDamage((int)(damage * DamageMultiplier));
        
    }
}
