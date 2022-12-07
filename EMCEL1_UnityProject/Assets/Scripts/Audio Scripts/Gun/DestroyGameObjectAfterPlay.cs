using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyGameObjectAfterPlay : MonoBehaviour
{
    private float totalTimeBeforeDestroy;
    void Start()
    {
        var sound = this.GetComponent<AudioSource>();
        totalTimeBeforeDestroy = sound.clip.length;
    }

    
    void Update()
    {
        totalTimeBeforeDestroy -= Time.deltaTime;

        if (totalTimeBeforeDestroy <= 0f)
            Destroy(this.gameObject);
    }
}
