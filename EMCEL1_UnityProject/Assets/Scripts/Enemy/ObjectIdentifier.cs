using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using Pathfinding;

public class ObjectIdentifier : MonoBehaviour
{
    public GameObject identifiedObj;

    private void Start()
    {
        GetComponentInParent<NavMeshAgent>().stoppingDistance = GetComponent<SphereCollider>().radius * 2;
    }
    private void OnTriggerStay(Collider other)
    {
        if (other != null)
        {
            identifiedObj = other.gameObject;
            GetComponentInParent<EnemyNavMeshScript>().objInRange = other.gameObject;
        }    
    }

    private void OnTriggerExit(Collider other)
    {
        if(other.gameObject.tag =="Player")
        {
            identifiedObj = null;
        }
    }
}
