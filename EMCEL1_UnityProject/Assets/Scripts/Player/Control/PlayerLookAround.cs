using UnityEngine;
using UnityEngine.InputSystem;

namespace Player.Control
{
    public class PlayerLookAround : MonoBehaviour
    {
        public Transform playerBody;
        public Vector2 clamp = new(-85, 85);
        [SerializeField] private float sensitivity = 5;
    
        public InputAction mouseX, mouseY;
        private float _xRotation;

        private void OnEnable()
        {
            mouseX.Enable();
            mouseY.Enable();
        }

        private void OnDisable()
        {
            mouseX.Disable();
            mouseY.Disable();
        }
    
        private void Update()
        {
            MouseLook();
        }

        private void MouseLook()
        {
            transform.Rotate(Vector3.up, mouseX.ReadValue<float>() * sensitivity * Time.deltaTime);
        
            float x = mouseX.ReadValue<float>() * sensitivity * Time.deltaTime;
            float y = mouseY.ReadValue<float>() * sensitivity * Time.deltaTime;

            _xRotation -= y;
            _xRotation = Mathf.Clamp(_xRotation, clamp.x, clamp.y);

            transform.localRotation = Quaternion.Euler(_xRotation, 0f, 0f);
            playerBody.Rotate(Vector3.up * x);
        }
    
    }
}
