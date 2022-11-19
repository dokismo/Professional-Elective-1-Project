using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Pathfinding;
using Player.Control;

public class SimpleEnemyColScript : MonoBehaviour
{
    [Header ("Attacking Variables")]
    public float defaultAttackSpeed = 5f, timeToAttack;
    public int enemyDamage = 10;
    public GameObject objInRange;

    public bool attacking = false;

    [Header("       Script For Referencing")]
    public AIDestinationSetter aiSetterScript;
    public AIPath aiPathScript;
    public SimpleEnemyColScript enemyColScript;
    public objectIdentifier objIdentifier;
    public forSpawningScript forSpawnScript;

    public SphereCollider objIdentifierSphere;


    private void Start()
    {
        transform.SetParent(GameObject.Find("Enemies").transform);

        aiSetterScript = transform.GetComponent<AIDestinationSetter>();
        aiPathScript = transform.GetComponent<AIPath>();
        enemyColScript = transform.GetComponent<SimpleEnemyColScript>();
        objIdentifier = transform.GetChild(0).GetComponent<objectIdentifier>();
        objIdentifierSphere = transform.GetChild(0).GetComponent<SphereCollider>();
        aiSetterScript.target = GameObject.Find("Player").transform;

        timeToAttack = defaultAttackSpeed;
        defaultAttackSpeed = enemyColScript.defaultAttackSpeed;
    }

  
    private void Update()
    {
        objIdentifierSphere.radius = aiPathScript.endReachedDistance;

        if (aiSetterScript.target != null && objIdentifier.identifiedObj != null)
        {
            if (aiPathScript.reachedEndOfPath && aiSetterScript.target.gameObject.tag == "Player"
                && objIdentifier.identifiedObj.tag == "Player")
            {
                attacking = true;
                enemyColScript.attacking = attacking;
            }
        }

        if (attacking)
        {
            if (timeToAttack <= 0)
            {
                timeToAttack = defaultAttackSpeed;
                aiPathScript.enabled = true;
                attacking = false;
                enemyColScript.attacking = attacking;
                enemyColScript.timeToAttack = enemyColScript.defaultAttackSpeed;
                enemyColScript.attackTarget();
            }
            else
            {
                aiPathScript.enabled = false;
                timeToAttack -= Time.deltaTime;
            }
        }
    }

 

    public void attackTarget()
    {
        if(objInRange != null)
        {
            if (objInRange.gameObject.tag == "Player")
            {
                PlayerStatus.changeHealth?.Invoke(enemyDamage);
                Debug.Log("ENEMY ATTACKING " + objInRange.gameObject.name);
                attacking = false;
            }
        } else
        {
            Debug.Log("Enemy attack MISSED!");
        }
    }
}
