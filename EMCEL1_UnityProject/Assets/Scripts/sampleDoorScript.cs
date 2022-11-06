using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class sampleDoorScript : MonoBehaviour
{
    float doorHealth = 20f;

    public void takeDamage(float dmg)
    {
        doorHealth -= dmg;
        checkHealth();
    }

    void checkHealth()
    {
        if(doorHealth <= 0f)
        {
            Destroy(gameObject);
        }
    }
}
