using System;
using UnityEngine;

namespace Player
{
    public class PlayerAnimation : MonoBehaviour
    {
        private SpriteRenderer spriteRenderer;
        private Rigidbody2D rb2d;
        private PlayerMovement playerMovement;

        private void Start()
        {
            rb2d = GetComponentInParent<Rigidbody2D>();
            spriteRenderer = GetComponent<SpriteRenderer>();
            playerMovement = GetComponentInParent<PlayerMovement>();
        }

        private void Update()
        {
            RotateSprite();
        }

        private void RotateSprite()
        {
            if (!playerMovement.IsMoving || rb2d.velocity.x == 0f) return;
            
            spriteRenderer.flipX = !(rb2d.velocity.x > 0);
        }
    }
}