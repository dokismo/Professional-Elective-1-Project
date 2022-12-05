using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Pathfinding;
using Pathfinding.Util;

public class SimpleAiScript : MonoBehaviour
{

    [Header("       Script For Referencing")]
    public AIDestinationSetter aiSetterScript;
    public AIPath aiPathScript;
    public SimpleEnemyColScript enemyColScript;
    public AttackRange objIdentifier;
    public ForSpawningScript forSpawnScript;

    public SphereCollider objIdentifierSphere;

    [Header("       Timers")]
    public float defaultAttackSpeed;

    [Header("       Enemy States")]
    public bool reachedEnd = false;
    public bool chasingPlayer = false;
    public bool attacking = false;


    void Start()
    {
        /*
        forSpawnScript = GameObject.Find("For Spawning").GetComponent<forSpawningScript>();
        aiSetterScript = transform.parent.GetComponent<AIDestinationSetter>();
        aiPathScript = transform.parent.GetComponent<AIPath>();
        enemyColScript = transform.parent.GetComponent<SimpleEnemyColScript>();
        objIdentifier = transform.parent.GetChild(1).GetComponent<objectIdentifier>();
        objIdentifierSphere = transform.parent.GetChild(1).GetComponent<SphereCollider>();
        aiSetterScript.target = GameObject.Find("Player").transform;
        transform.parent.SetParent(GameObject.Find("Enemies").transform);
        forSpawnScript.checkZombieCount();
        defaultAttackSpeed = enemyColScript.defaultAttackSpeed;
        */
    }

    private void Update()
    {

    }

}
