using UnityEngine;

namespace Player
{
    public class LookAtMouse : MonoBehaviour
    {
        private Rigidbody2D playerRigidbody2D;
        private SpriteRenderer spriteRenderer;
        private Vector2 mousePos;
        private Camera cam;

        private float appliedZ;
        
        private void Start()
        {
            playerRigidbody2D = GetComponentInParent<Rigidbody2D>();
            cam = Camera.main;
        }


        private void Update()
        {
            mousePos = cam.ScreenToWorldPoint(Input.mousePosition);
        }
        
        private void FixedUpdate()
        {
            Vector2 lookDir = mousePos - playerRigidbody2D.position;

            float angle = Mathf.Atan2(lookDir.y,lookDir.x)* Mathf.Rad2Deg;
            transform.rotation = Quaternion.Euler(0, 0, angle);
        }
    }
}