using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyBullet : MonoBehaviour
{
    private float boundary = 40f;
    private float boundaryNega = -40f;

    // Update is called once per frame
    void Update()
    {
        
        
        if(transform.position.x > boundary || transform.position.y > boundary)
        {
            Destroy(gameObject);
        }
        else if(transform.position.x < boundaryNega || transform.position.y < boundaryNega)
        {
            Destroy(gameObject);
        }
        
    }
}
