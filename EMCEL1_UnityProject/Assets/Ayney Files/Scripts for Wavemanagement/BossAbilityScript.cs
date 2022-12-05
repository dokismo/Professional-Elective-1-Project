using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using _2._5D_Objects;
public class BossAbilityScript : MonoBehaviour
{
    public Animator BossAnimator;
    NavMeshAgent BossNavmeshAgent;



    public float AbilityRange;
    SphereCollider BossAbilityCollider;

    public LayerMask RaycastIgnoreLayer;

    float BossSpeed;
    float AimTime = 2f;

    public float StunTime = 2f;

    public float ChargeLength = 3f;
    public float ChargeSpeed = 20f;
    public float ChargeDamage = 20f;


    public bool DoingAbility = false, Charging = false, isStunned = false;

    Transform target;
    public Transform ForwardTransform;

    Vector3 DashDirection;

    void Start()
    {

        ForwardTransform = transform.parent.Find("Sprite");
        BossNavmeshAgent = GetComponentInParent<NavMeshAgent>();

        BossNavmeshAgent.updateRotation = false;
        transform.parent.rotation = Quaternion.identity;

        BossAbilityCollider = transform.GetComponent<SphereCollider>();
        BossAbilityCollider.radius = AbilityRange;

        RaycastIgnoreLayer = 1 << LayerMask.NameToLayer("enemyLayer") | 1 << LayerMask.NameToLayer("detectPlayerLayer");

        BossSpeed = BossNavmeshAgent.speed;
    }

    void Update()
    {
        if(Charging)
        {
            BossNavmeshAgent.destination = Vector3.zero;

            transform.parent.transform.Translate(-ForwardTransform.forward * ChargeSpeed * Time.deltaTime);



        }

        if (isStunned)
        {
            BossNavmeshAgent.speed = 0f;
            ForwardTransform.GetComponent<SpriteBillboard>().enabled = false;
        }
       

        
    }

    private void OnTriggerStay(Collider other)
    {
        
        if(!isStunned)
        {
            if (other.transform.root.tag == "Player")
            {

                RaycastHit hit;
                Vector3 Direction = (other.transform.position - transform.position) + Vector3.up * 1;
                if (Physics.Raycast(transform.position, Direction, out hit, Mathf.Infinity, ~RaycastIgnoreLayer))
                {
                    if (hit.transform.tag == "Player" && !DoingAbility)
                    {
                        target = hit.collider.transform;
                        StartCoroutine(ChargeAbility());

                        DoingAbility = true;
                    }
                }
            }
        }
    }


    IEnumerator ChargeAbility()
    {
        //Freeze the boss.

        BossNavmeshAgent.speed = 0f;

        yield return new WaitForSeconds(AimTime);
        //for 2 points
        //DashDirection = transform.position - GameObject.Find("Player").transform.position;

        // Set rotation to look at player then freeze.
        ForwardTransform.GetComponent<SpriteBillboard>().enabled = false;
        BossNavmeshAgent.updateRotation = false;

        Charging = true;
        BossAnimator.SetBool("isCharging", true);
        yield return new WaitForSeconds(ChargeLength);

        // Set Everything back on
        BossNavmeshAgent.updateRotation = false;
        
        ForwardTransform.GetComponent<SpriteBillboard>().enabled = true;

        if(!isStunned) BossAnimator.SetBool("isCharging", false);



        Charging = false;

        BossNavmeshAgent.speed = BossSpeed;
        DoingAbility = false;
    }

    public IEnumerator StunTimer()
    {
        
        isStunned = true;
        yield return new WaitForSeconds(StunTime);
        ForwardTransform.GetComponent<SpriteBillboard>().enabled = true;
        BossNavmeshAgent.speed = BossSpeed;
        BossAnimator.SetBool("isCharging", Charging);
        isStunned = false;
    }
}
