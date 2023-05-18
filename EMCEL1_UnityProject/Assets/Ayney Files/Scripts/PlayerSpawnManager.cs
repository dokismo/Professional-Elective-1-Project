using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerSpawnManager : MonoBehaviour
{
    [SerializeField] GameObject PlayerPrefab;
    private void OnEnable()
    {
        PlayerUIHandler.onMainMenu += DestroyThis;
    }

    private void OnDisable()
    {
        PlayerUIHandler.onMainMenu -= DestroyThis;
    }
    void Awake()
    {
        if(GameObject.FindGameObjectWithTag("Player") == null)
        {
            Instantiate(PlayerPrefab);
        }
    }

    public void DestroyThis()
    {
        Destroy(gameObject);
    }
}
