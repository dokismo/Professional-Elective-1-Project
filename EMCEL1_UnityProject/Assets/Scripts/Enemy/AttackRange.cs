using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackRange : MonoBehaviour
{
    SphereCollider sphereCol;
    public float rangeRadius = 1.5f;
    public GameObject objInRange;

    private void Start()
    {
        sphereCol = GetComponent<SphereCollider>();

        sphereCol.radius = rangeRadius;
    }
    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.tag == "Player")
        {
            objInRange = other.gameObject;
            GetComponentInParent<SimpleEnemyColScript>().objInRange = objInRange;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if(other.gameObject.tag == "Player")
        {
            objInRange = null;
            GetComponentInParent<SimpleEnemyColScript>().objInRange = objInRange;
        }
    }
}
