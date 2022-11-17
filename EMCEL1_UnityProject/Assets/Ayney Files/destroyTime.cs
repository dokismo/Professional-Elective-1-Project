using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class destroyTime : MonoBehaviour
{
    public detectPlayerScript detectScript;
    public float timer = 5f;
    void Update()
    {
        if(timer > 0)
        {
            timer -= Time.deltaTime;
        }
        else
        {
            detectScript.patrol();
            Destroy(gameObject);
        }
    }
}