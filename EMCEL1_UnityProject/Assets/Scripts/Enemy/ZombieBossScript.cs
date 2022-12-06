using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class ZombieBossScript : MonoBehaviour
{
    public static Action onBossDeath;
    
    public float StartSpeed = 1f;
    public float MaxSpeed = 20f;

    private NavMeshAgent EnemyNMA;

    public float Acceleration = 2f;

    
    void Start()
    {
        EnemyNMA = transform.GetComponent<NavMeshAgent>();

    }

    // Update is called once per frame
    void Update()
    {
        IncrementingSpeed();
    }

    void IncrementingSpeed()
    {
        if (EnemyNMA.speed < MaxSpeed)
        {
            EnemyNMA.speed += Time.deltaTime * Acceleration;
        }
    }
}
