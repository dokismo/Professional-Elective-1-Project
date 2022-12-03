using System;
using SceneController;
using UnityEngine;
using UnityEngine.InputSystem;

namespace Player.Control
{
    public class PlayerTimeControl : MonoBehaviour
    {
        private bool isPaused;

        private void OnEnable()
        {
            GlobalCommand.setPause += SetPause;
        }

        private void OnDisable()
        {
            GlobalCommand.setPause -= SetPause;
        }

        private void Update()
        {
            if (Keyboard.current.escapeKey.wasPressedThisFrame || Keyboard.current.kKey.wasPressedThisFrame)
            {
                Debug.Log("test");
                GlobalCommand.setPause?.Invoke(!isPaused);
            }
        }

        private void SetPause(bool value) => isPaused = value;
    }
}