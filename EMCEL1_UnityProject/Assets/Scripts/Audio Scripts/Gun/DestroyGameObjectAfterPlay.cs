using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyGameObjectAfterPlay : MonoBehaviour
{
    private float totalTimeBeforeDestroy;

    private AudioSource audioSource;
    
    void Start()
    {
        audioSource = GetComponent<AudioSource>();
        totalTimeBeforeDestroy = audioSource.clip.length;
    }
    
    void Update()
    {
        totalTimeBeforeDestroy -= Time.deltaTime;

        if (totalTimeBeforeDestroy <= 0f)
            Destroy(gameObject);
    }
}
