using System;
using Player.Control;
using UnityEngine;
using UnityEngine.InputSystem;

namespace Item.MedKit
{
    public class MedKit : MonoBehaviour
    {
        public static Action itemHeal;
        public float enableTime = 0.5f;
        public int healAmount;

        public bool CanHeal => enableTimer <= 0;
        private float enableTimer;

        private void OnEnable()
        {
            enableTimer = enableTime;
        }

        private void Update()
        {
            enableTimer = Mathf.Clamp(enableTimer - Time.deltaTime, 0, enableTime);
            if (Mouse.current.leftButton.wasPressedThisFrame) Heal();
        }

        private void Heal()
        {
            if (!CanHeal) return;
            
            PlayerStatus.changeHealth?.Invoke(healAmount);
            gameObject.SetActive(false);
            itemHeal?.Invoke();
        }
    }
}