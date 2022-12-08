using System;
using Core;
using UI;
using UnityEngine;

public class Destroyer : MonoBehaviour
{
    private UILoadScene uiLoadScene;

    private void Start()
    {
        uiLoadScene = GetComponent<UILoadScene>();
    }

    private void OnTriggerEnter(Collider other)
    {
        uiLoadScene.LoadSingle();
    }
}
