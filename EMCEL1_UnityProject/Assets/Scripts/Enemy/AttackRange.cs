using UnityEngine;
using UnityEngine.AI;

public class AttackRange : MonoBehaviour
{
    public GameObject identifiedObj;
    public float AttackRangeRadius;

    // Added by jay for optimization reasons
    private NavMeshAgent navMeshAgent;
    private GameObject target;
    
    private void Start()
    {
        transform.GetComponent<SphereCollider>().radius = AttackRangeRadius;
        navMeshAgent = GetComponentInParent<NavMeshAgent>();
        target = GameObject.FindGameObjectWithTag("Player");
    }

    private void Update()
    {
        target ??= GameObject.FindGameObjectWithTag("Player");
    }

    private void OnTriggerStay(Collider other)
    {
        if (other.gameObject == target && navMeshAgent.isOnNavMesh)
        {
            navMeshAgent.isStopped = true;
            identifiedObj = other.gameObject;
        }    
    }

    private void OnTriggerExit(Collider other)
    {
        if(other.gameObject == target)
        {
            navMeshAgent.isStopped = false;
            identifiedObj = null;
        }
    }
}
