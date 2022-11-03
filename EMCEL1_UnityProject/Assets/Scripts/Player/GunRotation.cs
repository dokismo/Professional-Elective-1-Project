using System;
using UnityEngine;

namespace Player
{
    public class GunRotation : MonoBehaviour
    {
        [SerializeField]
        private Rigidbody2D rb2d;
        

        [SerializeField] private float lowestZ = -90, highestZ = 90;

        private SpriteRenderer spriteRenderer;
        private Vector2 mousePos;
        private Camera cam;
        
        private void Start()
        {
            spriteRenderer = GetComponent<SpriteRenderer>();
            cam = Camera.main;
        }

        private void Update()
        {
            mousePos = cam.ScreenToWorldPoint(Input.mousePosition);
            
            FlipSprite();
        }

        private void FlipSprite()
        {
            var z = transform.rotation.eulerAngles.z;

            spriteRenderer.flipY = z > lowestZ && z < highestZ;
        }
        
        
        private void FixedUpdate()
        {
            Vector2 lookDir = mousePos - rb2d.position;

            float angle = Mathf.Atan2(lookDir.y,lookDir.x)* Mathf.Rad2Deg;
            transform.rotation = Quaternion.Euler(0, 0, angle);
        }

        
    }
}