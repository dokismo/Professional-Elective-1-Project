using System;
using CORE;
using UnityEngine;

namespace Environment.Objects
{
    public class Door : MonoBehaviour, IInteract
    {
        public GameObject open, close;

        private void Update()
        {
            if (Input.GetKeyDown(KeyCode.L))
            {
                Toggle();
            }
        }

        public void Toggle()
        {
            if (open.activeSelf)
            {
                open.SetActive(false);
                close.SetActive(true);
            }
            else
            {
                open.SetActive(true);
                close.SetActive(false);
            }
        }
        
        public void Interact()
        {
            Toggle();
        }
    }
}