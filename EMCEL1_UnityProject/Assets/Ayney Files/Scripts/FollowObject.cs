using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowObject : MonoBehaviour
{
    [SerializeField] Transform Object;
    void Update()
    {
        transform.position = Object.position;
    }
}
