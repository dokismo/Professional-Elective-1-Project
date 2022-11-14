using System;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.InputSystem;

namespace Player.Control
{
    public class Shooting : MonoBehaviour
    {
        public float distance;
        public LayerMask targetLayers;
        
        public InputAction fireInput;

        private Camera thisCamera;

        private void Start()
        {
            thisCamera = Camera.main;
        }

        private void OnEnable()
        {
            fireInput.Enable();
        }

        private void OnDisable()
        {
            fireInput.Disable();
        }

        private void Update()
        {
            if (Mouse.current.leftButton.wasPressedThisFrame)
            {
                Fire();
            }
        }

        private void Fire()
        {
            if (!PlayerStatus.canShoot) return;
            
            Ray ray = thisCamera.ScreenPointToRay(Mouse.current.position.ReadValue());

            if (Physics.Raycast(ray, out var raycastHit, distance, targetLayers))
            {
                if (raycastHit.collider.GetComponent<NavMeshAgent>() != null)
                {
                    Destroy(raycastHit.collider.gameObject);
                }
            }
        }
    }
}
