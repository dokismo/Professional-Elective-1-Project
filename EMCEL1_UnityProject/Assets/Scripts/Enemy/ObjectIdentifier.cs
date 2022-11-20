using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectIdentifier : MonoBehaviour
{
    public GameObject identifiedObj;

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
