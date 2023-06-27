using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RandomPitchOnSpawn : MonoBehaviour
{
    [SerializeField] float MinPitch, MaxPitch;
    void Awake()
    {
        GetComponent<AudioSource>().pitch = Random.Range(MinPitch, MaxPitch);
    }

   
}
