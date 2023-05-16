using System;
using Core;
using UI;
using UnityEngine;

public class Destroyer : MonoBehaviour
{
    private UILoadScene uiLoadScene;

    public string player = "Player";

    private void Start()
    {
        uiLoadScene = GetComponent<UILoadScene>();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.transform.CompareTag(player))
            uiLoadScene.LoadSingle();
        else
            Destroy(other.gameObject);
    }
}
