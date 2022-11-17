using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Pathfinding;

public class SimpleEnemyColScript : MonoBehaviour
{
    public float enemyDamage = 1f, attackSpeed = 1f;
    bool attacking = false;
    detectPlayerScript script1;
    Collision collObj;

    private void Start()
    {
        script1 = GetComponentInChildren<detectPlayerScript>();
    }
    private void Update()
    {
        if (attacking)
        {
            if (attackSpeed > 0)
            {
                attackSpeed -= Time.deltaTime;
            }
            else
            {
                attacking = false;
                attackSpeed = 1f;
            }
        }
    }

    private void OnCollisionStay(Collision collision)
    {

        if (collision.gameObject.tag == "Door" && !attacking && !script1.patrolling)
        {
            collObj = collision;
            attackTarget();
            attacking = true;
        }

        if (collision.gameObject.tag == "Player" && !attacking)
        {
            collObj = collision;
            attackTarget();
            attacking = true;
        }



    }

    public void attackTarget()
    {

        if (collObj.gameObject.tag == "Player")
        {
            Debug.Log("ENEMY ATTACKING " + collObj.gameObject.name);
        }
    }
}
