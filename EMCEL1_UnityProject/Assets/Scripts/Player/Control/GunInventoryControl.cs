using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

namespace Player.Control
{
    public class GunInventoryControl : MonoBehaviour
    {
        public PlayerStatusScriptable playerStatusScriptable;

        private List<GameObject> guns => playerStatusScriptable.guns;

        private void Start()
        {
            foreach (var gun in guns)
            {
                gun.SetActive(false);
            }
            
            playerStatusScriptable.SetAnchor(Camera.main.gameObject);
        }

        private void Update()
        {
            Selection();
        }

        private void Selection()
        {
            if (Keyboard.current.digit1Key.wasPressedThisFrame)
                playerStatusScriptable.Switch(0);
            else if (Keyboard.current.digit2Key.wasPressedThisFrame)
                playerStatusScriptable.Switch(1);
            else if (Keyboard.current.digit3Key.wasPressedThisFrame) 
                playerStatusScriptable.Switch(2);
        }

        
    }
}