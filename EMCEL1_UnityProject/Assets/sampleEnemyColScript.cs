using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Pathfinding;

public class sampleEnemyColScript : MonoBehaviour
{
    public float enemyDamage = 1f, attackSpeed = 1f;
    bool attacking = false;
    detectPlayerScript script1;
    AIDestinationSetter script2;
    Collision2D collObj;

    private void Start()
    {
        script1 = GetComponentInChildren<detectPlayerScript>();
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
    private void OnCollisionStay2D(Collision2D collision)
    {
        
        if(collision.gameObject.tag == "Door" && !attacking && !script1.patrolling)
        {
            Debug.Log("COLLIDED WITH " + collision.gameObject.name);
            collObj = collision;
            attackTarget();
            attacking = true;
        }

        if(collision.gameObject.tag == "Player" && !attacking)
        {
            Debug.Log("COLLIDED WITH " + collision.gameObject.name);
            collObj = collision;
            attackTarget();
            attacking = true;
        }

        /*if (collision.gameObject == script1.instantiatedLastPost)
        {
            

            if(script1 !=null)
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
        }*/


    }

    private void OnTriggerEnter2D(Collider2D collision)
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
        if(collObj.gameObject.tag == "Door")
        {
            collObj.gameObject.GetComponent<sampleDoorScript>().takeDamage(enemyDamage);

            Debug.Log("ENEMY ATTACKING " + collObj.gameObject.name);
        }

        if(collObj.gameObject.tag == "Player")
        {
            Debug.Log("ENEMY ATTACKING " + collObj.gameObject.name);
        }
    }
}
