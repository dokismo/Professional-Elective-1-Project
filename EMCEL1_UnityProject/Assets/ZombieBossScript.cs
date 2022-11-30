using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Pathfinding;

public class ZombieBossScript : MonoBehaviour
{
    AIPath AIPathScript;

    public float StartSpeed = 1f;
    public float MaxSpeed = 20f;

    public float Acceleration = 2f;

    
    void Start()
    {
        AIPathScript = transform.GetComponent<AIPath>();

    }

    // Update is called once per frame
    void Update()
    {
        IncrementingSpeed();
    }

    void IncrementingSpeed()
    {
        if (AIPathScript.maxSpeed < MaxSpeed)
        {
            AIPathScript.maxSpeed += Time.deltaTime * Acceleration;
        }
    }
}
