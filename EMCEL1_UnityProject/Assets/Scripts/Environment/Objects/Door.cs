using System;
using CORE;
using UnityEngine;

namespace Environment.Objects
{
    [RequireComponent(typeof(Collider2D))]
    public class Door : MonoBehaviour, IInteract
    {
        [SerializeField]
        private bool isOpen;
        
        private SpriteRenderer spriteRenderer;
        private Collider2D col2D;

        private void Start()
        {
            spriteRenderer = GetComponent<SpriteRenderer>();
            col2D = GetComponent<Collider2D>();
            SetDoor();
        }

        private void Update()
        {
            if (Input.GetKeyDown(KeyCode.Space)) Interact();
        }

        public void Interact()
        {
            isOpen = !isOpen;
            SetDoor();
        }

        private void SetDoor()
        {
            col2D.enabled = !isOpen;
            Color color = spriteRenderer.color;
            
            spriteRenderer.color = isOpen ? new Color(color.r, color.g, color.b, 0.25f) : new Color(color.r, color.g, color.b, 1);
        }
    }
}