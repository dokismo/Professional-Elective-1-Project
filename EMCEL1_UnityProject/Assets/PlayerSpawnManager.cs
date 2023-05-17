using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerSpawnManager : MonoBehaviour
{
    [SerializeField] GameObject PlayerPrefab;
    void Awake()
    {
        if(GameObject.FindGameObjectWithTag("Player") == null)
        {
            Instantiate(PlayerPrefab);
        }
    }
}
