using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Pathfinding;
using Pathfinding.Util;


public class DetectPlayerScript : MonoBehaviour
{
    
    [Header("       Layers for raycast to ignore")]
    public LayerMask IgnoreMe;
    public LayerMask IgnoreMe2;
    public LayerMask finalIgnoreLayer;

    [Header("       Script For Referencing")]
    public AIDestinationSetter aiSetterScript;
    public AIPath aiPathScript;
    public sampleEnemyColScript enemyColScript;
    public AttackRange objIdentifier;

    public GameObject lastPos, instantiatedLastPost;
    public GameObject[] patrolSpots;
    public SphereCollider objIdentifierSphere;

    [Header("       Timers")]
    public float stopToAttackTime = 2f;
    public float stayInLastPos = 2f;
    public float chaseTime = 1f;
    public float timeToStand = 2f;

    [Header("       Enemy States")]
    public bool reachedEnd = false;
    public bool canRaycast = true;
    public bool chasingPlayer = false;
    public bool findingPlayer = false;
    public bool playerDetected = false;
    public bool goingToLastPos = false;
    public bool patrolling = true;
    public bool lastPosInstantiated = false;
    public bool attacking = false;


    RaycastHit ray;
    void Start()
    {

        aiSetterScript = transform.parent.GetComponent<AIDestinationSetter>();
        aiPathScript = transform.parent.GetComponent<AIPath>();
        enemyColScript = transform.parent.GetComponent<sampleEnemyColScript>();
        objIdentifier = transform.parent.GetChild(1).GetComponent<AttackRange>();
        objIdentifierSphere = transform.parent.GetChild(1).GetComponent<SphereCollider>();
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
        reachedEnd = aiPathScript.reachedEndOfPath;
        objIdentifierSphere.radius = aiPathScript.endReachedDistance;

        if(objIdentifier.identifiedObj != null)
        {
            
        }

        if (aiSetterScript.target != null && objIdentifier.identifiedObj != null)
        {
            if (aiPathScript.reachedEndOfPath && aiSetterScript.target.gameObject.tag == "Player" && !patrolling 
                && objIdentifier.identifiedObj.tag =="Player")
            {
                attacking = true;
            }
        }

        if(attacking)
        {
            if(stopToAttackTime <= 0)
            {
                stopToAttackTime = 2f;
                attacking = false;
            } else
            {
                aiPathScript.enabled = false;
                stopToAttackTime -= Time.deltaTime;
            }
        }



        if (findingPlayer && !playerDetected && !goingToLastPos)
        {
            chaseTime -= Time.deltaTime;
            aiPathScript.endReachedDistance = 2f;
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
    
    private void OnTriggerStay(Collider collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            if (canRaycast)
            {
                Debug.DrawRay(transform.parent.position, GameObject.Find("Player").transform.position - transform.parent.position);
              
                Physics.Raycast(transform.parent.position, GameObject.Find("Player").transform.position - transform.parent.position,out ray, Mathf.Infinity,~finalIgnoreLayer);
            }
            

            if(ray.collider.tag == "Player")
            {
                patrolling = false;
                
                if(!attacking)
                {
                    timeToStand = 2f;
                    aiPathScript.enabled = true;
                    aiSetterScript.target = ray.collider.gameObject.transform;
                    chasingPlayer = true;
                    findingPlayer = false;
                    goingToLastPos = false;
                    playerDetected = true;
                    if (instantiatedLastPost != null)
                    {
                        Destroy(instantiatedLastPost);
                        lastPosInstantiated = false;
                    }
                    aiPathScript.endReachedDistance = 2f;
                    chaseTime = 1f;
                }
                

            } else if(ray.collider.tag != "Door" && chasingPlayer)
            {
                findingPlayer = true;
                chasingPlayer = false;
                playerDetected = false;
            }
        }
    }

    private void OnTriggerExit(Collider collision)
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
        if (!lastPosInstantiated && !playerDetected )
        {
            aiPathScript.enabled = true;
            instantiatedLastPost = Instantiate(lastPos, GameObject.FindGameObjectWithTag("Player").transform.position, Quaternion.identity);
            instantiatedLastPost.GetComponent<DestroyTime>().detectScript = gameObject.GetComponent<DetectPlayerScript>();
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
        attacking = false;
        stopToAttackTime = 0;
        if (!playerDetected)
        {
            chasingPlayer = false;
            goingToLastPos = false;
            patrolling = true;
            lastPosInstantiated = false;
            aiPathScript.enabled = true;

            aiPathScript.endReachedDistance = 0.8f;

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
