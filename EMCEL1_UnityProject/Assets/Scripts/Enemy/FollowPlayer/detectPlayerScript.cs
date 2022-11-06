using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Pathfinding;
using Pathfinding.Util;


public class detectPlayerScript : MonoBehaviour
{
    public LayerMask IgnoreMe, IgnoreMe2, finalIgnoreLayer;

    public AIDestinationSetter aiSetterScript;
    public AIPath aiPathScript;
    public sampleEnemyColScript enemyColScript;

    public GameObject lastPos, instantiatedLastPost;



    public GameObject[] patrolSpots;

    public float stopToAttackTime = 2f, stayInLastPos = 2f, chaseTime = 1f, timeToStand = 2f;

    public bool reachedEnd = false, canRaycast = true, chasingPlayer = false, findingPlayer = false,
        playerDetected = false, goingToLastPos = false, patrolling = true, lastPosInstantiated = false;

    RaycastHit2D ray;
    void Start()
    {
        aiSetterScript = transform.parent.GetComponent<AIDestinationSetter>();
        aiPathScript = transform.parent.GetComponent<AIPath>();
        enemyColScript = transform.parent.GetComponent<sampleEnemyColScript>();
        IgnoreMe = 1 << LayerMask.NameToLayer("enemyLayer");
        IgnoreMe2 = 1 << LayerMask.NameToLayer("detectPlayerLayer");
        finalIgnoreLayer = IgnoreMe | IgnoreMe2;

    }

    private void Update()
    {
        if(instantiatedLastPost == null)
        {
            lastPosInstantiated = false;
        }
        if (!reachedEnd && aiPathScript.enabled)
        {
            reachedEnd = aiPathScript.reachedEndOfPath;
        }

        

        if (aiPathScript.reachedEndOfPath)
        {
            aiPathScript.enabled = false;
        }

        if (aiPathScript.reachedEndOfPath && aiSetterScript.target.gameObject.tag == "Player")
        {
            
            aiPathScript.enabled = false;

        }

        if (!aiPathScript.enabled && !patrolling && !goingToLastPos)
        {
            stopToAttackTime -= Time.deltaTime;

            if(stopToAttackTime <= 0)
            {
                aiPathScript.enabled = true;
                stopToAttackTime = 2f;
            }
        }



        if(findingPlayer && !playerDetected && !goingToLastPos)
        {
            Debug.Log("CHASE TIME STARTED");
            chaseTime -= Time.deltaTime;
            aiPathScript.endReachedDistance = 0.5f;
            if (chaseTime <= 0)
            {
                goToLastPos();
                chaseTime = 1f;
                findingPlayer = false;
            }
        }


        if(patrolling && !playerDetected)
        {
            patrol();
        }



    }
    
    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            
            if (canRaycast)
            {
                Debug.Log("Player Detected");
                Debug.DrawRay(transform.parent.position, GameObject.Find("Player").transform.position - transform.parent.position);
                ray = Physics2D.Raycast(transform.parent.position, GameObject.Find("Player").transform.position - transform.parent.position, Mathf.Infinity,~finalIgnoreLayer);
                Debug.Log("Ray detected:  " + ray.collider.name);
            }
            

            if(ray.collider.tag == "Player")
            {
                timeToStand = 2f;
                aiPathScript.enabled = true;
                aiSetterScript.target = ray.collider.gameObject.transform;
                chasingPlayer = true;
                findingPlayer = false;
                patrolling = false;
                goingToLastPos = false;
                playerDetected = true;

                if (instantiatedLastPost != null)
                {
                    Destroy(instantiatedLastPost);
                    lastPosInstantiated = false;
                }
                
               
                aiPathScript.endReachedDistance = 1f;
                chaseTime = 1f;
                

            } else if(ray.collider.tag != "Door" && chasingPlayer)
            {
                findingPlayer = true;
                chasingPlayer = false;
                playerDetected = false;
            }
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if(collision.gameObject.tag=="Player")
        {
            findingPlayer = true;
            chasingPlayer = false;
            playerDetected = false;
        }
    }

    public void goToLastPos()
    {
        if (!lastPosInstantiated && !playerDetected)
        {
            aiPathScript.enabled = true;
            instantiatedLastPost = Instantiate(lastPos, GameObject.FindGameObjectWithTag("Player").transform.position, Quaternion.identity);
            instantiatedLastPost.GetComponent<destroyTime>().detectScript = gameObject.GetComponent<detectPlayerScript>();
            aiSetterScript.target = instantiatedLastPost.transform;
            goingToLastPos = true;
            lastPosInstantiated = true;
        } else 
        {
            patrol();
        }
    }

    public void patrol()
    {
        if (!playerDetected)
        {
            chasingPlayer = false;
            goingToLastPos = false;
            patrolling = true;
            lastPosInstantiated = false;
            aiPathScript.enabled = true;

            aiPathScript.endReachedDistance = 0.5f;

            Debug.Log("PATROLLING");
            if (timeToStand > 0)
            {
                timeToStand -= Time.deltaTime;
            }
            else
            {

                aiSetterScript.target = patrolSpots[Random.Range(0, patrolSpots.Length)].gameObject.transform;
                timeToStand = 2f;
            }
        }
        
    }
}
