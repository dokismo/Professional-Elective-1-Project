using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using Pathfinding;

public class AttackRange : MonoBehaviour
{
    public GameObject identifiedObj;
    public float AttackRangeRadius;
    private void Start()
    {
        transform.GetComponent<SphereCollider>().radius = AttackRangeRadius;
    }
    private void OnTriggerStay(Collider other)
    {
        if (other != null)
        {
            GetComponentInParent<EnemyNavMeshScript>().objInRange = other.gameObject;
            transform.parent.GetComponent<NavMeshAgent>().isStopped = true;
            identifiedObj = other.gameObject;
            GetComponentInParent<EnemyNavMeshScript>().objInRange = other.gameObject;
        }    
    }

    private void OnTriggerExit(Collider other)
    {
        if(other.gameObject.tag =="Player")
        {
            GetComponentInParent<EnemyNavMeshScript>().objInRange = null;
            transform.parent.GetComponent<NavMeshAgent>().isStopped = false;
            identifiedObj = null;
        }
    }
}
