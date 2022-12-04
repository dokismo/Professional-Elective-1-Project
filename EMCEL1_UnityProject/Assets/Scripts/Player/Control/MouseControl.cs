using System;
using SceneController;
using UnityEngine;

namespace Player.Control
{
    public class MouseControl : MonoBehaviour
    {
        private void OnEnable()
        {
            GlobalCommand.setPause += SetMouseControl;
        }

        private void OnDisable()
        {
            GlobalCommand.setPause -= SetMouseControl;
        }

        private void OnDestroy()
        {
            SetMouseControl(true);
        }

        private void Start()
        {
            Cursor.lockState = CursorLockMode.Locked;
        }

        private void SetMouseControl(bool value)
        {
            Cursor.lockState = value
                ? CursorLockMode.None
                : CursorLockMode.Locked;
        }
    }
}