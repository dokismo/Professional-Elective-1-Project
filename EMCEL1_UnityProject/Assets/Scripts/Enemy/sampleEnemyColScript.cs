using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Pathfinding;

public class sampleEnemyColScript : MonoBehaviour
{
    public float enemyDamage = 1f, attackSpeed = 1f;
    bool attacking = false;
    DetectPlayerScript script1;
    AIDestinationSetter script2;
    Collision collObj;

    private void Start()
    {
        script1 = GetComponentInChildren<DetectPlayerScript>();
        script2 = GetComponentInParent<AIDestinationSetter>();
    }
    private void Update()
    {
        if(attacking)
        {
            if(attackSpeed > 0)
            {
                attackSpeed -= Time.deltaTime;
            } else
            {
                attacking = false;
                attackSpeed = 1f;
            }
        }
    }
    
    private void OnCollisionStay(Collision collision)
    {
        
        if(collision.gameObject.tag == "Door" && !attacking && !script1.patrolling)
        {
            collObj = collision;
            attackTarget();
            attacking = true;
        }

        if(collision.gameObject.tag == "Player" && !attacking)
        {
            collObj = collision;
            attackTarget();
            attacking = true;
        }



    }

    private void OnTriggerEnter(Collider collision)
    {
        if (collision.gameObject == script1.instantiatedLastPost)
        {


            if (script1 != null)
            {
                script1.patrolling = true;
                script1.lastPosInstantiated = false;
                script1.goingToLastPos = false;
            }
            if (script2 != null)
            {
                script2.target = null;
            }
            Destroy(script1.instantiatedLastPost);
        }
    }



    public void attackTarget()
    {

        if(collObj.gameObject.tag == "Player")
        {
            
        }
    }
}
