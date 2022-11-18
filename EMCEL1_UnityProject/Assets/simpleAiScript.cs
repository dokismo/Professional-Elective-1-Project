using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Pathfinding;
using Pathfinding.Util;

public class simpleAiScript : MonoBehaviour
{

    [Header("       Script For Referencing")]
    public AIDestinationSetter aiSetterScript;
    public AIPath aiPathScript;
    public sampleEnemyColScript enemyColScript;
    public objectIdentifier objIdentifier;
    public forSpawningScript forSpawnScript;

    public SphereCollider objIdentifierSphere;

    [Header("       Timers")]
    public float stopToAttackTime = 2f;

    [Header("       Enemy States")]
    public bool reachedEnd = false;
    public bool chasingPlayer = false;
    public bool attacking = false;


    void Start()
    {
        forSpawnScript = GameObject.Find("For Spawning").GetComponent<forSpawningScript>();
        aiSetterScript = transform.parent.GetComponent<AIDestinationSetter>();
        aiPathScript = transform.parent.GetComponent<AIPath>();
        enemyColScript = transform.parent.GetComponent<sampleEnemyColScript>();
        objIdentifier = transform.parent.GetChild(1).GetComponent<objectIdentifier>();
        objIdentifierSphere = transform.parent.GetChild(1).GetComponent<SphereCollider>();
        aiSetterScript.target = GameObject.Find("Player").transform;
        transform.parent.SetParent(GameObject.Find("Enemies").transform);
        forSpawnScript.checkZombieCount();
    }

    private void Update()
    {
        reachedEnd = aiPathScript.reachedEndOfPath;
        objIdentifierSphere.radius = aiPathScript.endReachedDistance;

        if (aiSetterScript.target != null && objIdentifier.identifiedObj != null)
        {
            if (aiPathScript.reachedEndOfPath && aiSetterScript.target.gameObject.tag == "Player"
                && objIdentifier.identifiedObj.tag == "Player")
            {
                attacking = true;
            }
        }

        if (attacking)
        {
            if (stopToAttackTime <= 0)
            {
                stopToAttackTime = 2f;
                aiPathScript.enabled = true;
                attacking = false;
            }
            else
            {
                aiPathScript.enabled = false;
                stopToAttackTime -= Time.deltaTime;
            }
        }
    }

}
