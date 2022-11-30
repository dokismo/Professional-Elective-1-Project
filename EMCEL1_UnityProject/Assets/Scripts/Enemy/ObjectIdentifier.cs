using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Pathfinding;

public class ObjectIdentifier : MonoBehaviour
{
    public GameObject identifiedObj;

    private void Start()
    {
        GetComponentInParent<AIPath>().endReachedDistance = GetComponent<SphereCollider>().radius;
    }
    private void OnTriggerStay(Collider other)
    {
        if (other != null)
        {
            identifiedObj = other.gameObject;
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
