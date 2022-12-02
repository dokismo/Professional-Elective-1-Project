using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using Player.Control;

public class EnemyNavMeshScript : MonoBehaviour
{
    private NavMeshAgent EnemyNMAgent;

   
    [Header("Attacking Variables")]
    public float defaultAttackSpeed = 5f, timeToAttack;
    public int enemyDamage = 10;
    public GameObject objInRange;

    public bool attacking = false;

    [Header("Script For Referencing")]
    public ObjectIdentifier objIdentifier;
    public ForSpawningScript forSpawnScript;

    public SphereCollider objIdentifierSphere;

    void Start()
    {
        EnemyNMAgent = transform.GetComponent<NavMeshAgent>();
        transform.SetParent(GameObject.Find("Enemies").transform);

        objIdentifier = transform.GetChild(0).GetComponent<ObjectIdentifier>();
        objIdentifierSphere = transform.GetChild(0).GetComponent<SphereCollider>();

        timeToAttack = defaultAttackSpeed;
    }

    // Update is called once per frame
    void Update()
    {
        if(EnemyNMAgent.isStopped == false && !attacking)
        {
            EnemyNMAgent.destination = GameObject.Find("Player").transform.position;
        }
        else
        {
            EnemyNMAgent.destination = transform.position;
            EnemyNMAgent.isStopped = true;
        }


        if (EnemyNMAgent.enabled && objIdentifier.identifiedObj != null)
        {
          
            if (EnemyNMAgent.remainingDistance <= EnemyNMAgent.stoppingDistance && objIdentifier.identifiedObj.tag == "Player")
            {
                attacking = true;
            }
        }

        if (attacking)
        {
            if (timeToAttack <= 0)
            {
                timeToAttack = defaultAttackSpeed;
                EnemyNMAgent.isStopped = false;
                attacking = false;
                timeToAttack = defaultAttackSpeed;
                attackTarget();
            }
            else
            {
                EnemyNMAgent.destination = transform.position;
                EnemyNMAgent.destination = GameObject.Find("Player").transform.position;
                timeToAttack -= Time.deltaTime;
            }
        }
    }


    public void attackTarget()
    {
        if (objInRange != null)
        {
            if (objInRange.gameObject.tag == "Player")
            {
                PlayerStatus.changeHealth?.Invoke(-enemyDamage);
                attacking = false;
            }
        }
        else
        {
            Debug.Log("Enemy attack MISSED!");
        }

        if (GetComponent<ZombieBossScript>() != null)
        {
            EnemyNMAgent.speed = GetComponent<ZombieBossScript>().StartSpeed;
        }
    }
}
