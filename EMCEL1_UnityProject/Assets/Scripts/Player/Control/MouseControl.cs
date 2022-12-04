using System;
using SceneController;
using UI;
using UnityEngine;

namespace Player.Control
{
    public class MouseControl : MonoBehaviour
    {
        private void OnEnable()
        {
            GlobalCommand.setPause += SetMouseControl;
            DisplayStatus.onDead += OnDeath;
        }

        private void OnDisable()
        {
            GlobalCommand.setPause -= SetMouseControl;
            DisplayStatus.onDead -= OnDeath;
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

        private void OnDeath()
        {
            SetMouseControl(true);
        }
    }
}