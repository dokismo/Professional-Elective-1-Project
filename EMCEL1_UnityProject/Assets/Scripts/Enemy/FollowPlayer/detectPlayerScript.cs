using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Pathfinding;

public class detectPlayerScript : MonoBehaviour
{
    public AIDestinationSetter aiSetterScript;
    void Start()
    {
        aiSetterScript = transform.parent.GetComponent<AIDestinationSetter>();
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            Debug.Log("Player Detected");

            Debug.DrawRay(transform.parent.position, GameObject.Find("Player").transform.position - transform.parent.position);
            RaycastHit2D ray = Physics2D.Raycast(transform.parent.position, GameObject.Find("Player").transform.position - transform.parent.position);

            Debug.Log("RAY HIT!: " + ray.collider.gameObject.name);

            if(ray.collider.tag == "Player")
            {
                aiSetterScript.target = GameObject.Find("Player").transform;
            }

            
            

            //aiSetterScript.target = GameObject.Find("Player").transform;
        }
    }
}
