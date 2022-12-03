using System;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.Serialization;

namespace Player.Control
{
    [RequireComponent(typeof(CharacterController))]
    public class Movement : MonoBehaviour
    {
        public delegate Movement GetEvent();
        public static GetEvent getMovement;
        
        public delegate void OnAction();
        public static OnAction onJump;
        public static OnAction onLand;
        
        [Header("Spec")]
        public float speed = 8f;
        public float gravity = -9.81f;
        public float jumpHeight = 3f;
        public float onLandWait = 0.6f;
        
        [Header("Setting")]
        public Transform groundCheck;
        public Transform cameraAnchor;
        public float groundDistance = 0.4f;
        public LayerMask groundLayer;
        [FormerlySerializedAs("inputAction")] 
        public InputAction movementInput;

        [HideInInspector] public Vector3 moveDir;
        [HideInInspector] public Vector3 velocity;
        private CharacterController controller;
        [HideInInspector] public bool isGrounded;
        private float timer;

        private Vector3 CamAncForward => new(cameraAnchor.forward.x, 0, cameraAnchor.forward.z);
        private Vector3 CamAncRight => new(cameraAnchor.right.x, 0, cameraAnchor.right.z);

        public bool CanMove => timer <= 0;
        public bool IsMoving => moveDir.magnitude > 0.1f;
        public bool IsFalling => velocity.y < -0.02f;

        private void OnEnable()
        {
            movementInput.Enable();
            getMovement += GetMoveMagnitude;
        }

        private void OnDisable()
        {
            movementInput.Disable();
            getMovement -= GetMoveMagnitude;
        }

        private Movement GetMoveMagnitude() => this;
        

        private void Start()
        {
            timer = 0f;
            controller = GetComponent<CharacterController>();
        }

        private void OnDrawGizmosSelected()
        {
            Gizmos.DrawWireSphere(groundCheck.position, groundDistance);
        }

        private void Update()
        {
            timer = Mathf.Clamp(timer - Time.deltaTime, 0, 20);
            Vector3 inputs = movementInput.ReadValue<Vector3>();

            moveDir =
                CamAncRight * inputs.x +
                CamAncForward * inputs.z;

            bool checkFloor = Physics.CheckSphere(groundCheck.position, groundDistance, groundLayer);
            
            CheckFloor(checkFloor, inputs);
            
            isGrounded = checkFloor;
        }

        private void FixedUpdate()
        {
            velocity.y += gravity * Time.fixedDeltaTime;
            Debug.Log($"{Time.fixedDeltaTime} && {Time.deltaTime}");

            controller.Move(velocity + moveDir.normalized * (speed * Time.fixedDeltaTime));
        }

        private void CheckFloor(bool checkFloor, Vector3 inputs)
        {
            if (!checkFloor) return;
                
            if (velocity.y < 0)
                velocity.y = -0.01f;

            if (inputs.y > 0 && CanMove)
            {
                velocity.y = Mathf.Sqrt(jumpHeight * -2f * gravity);
                onJump?.Invoke();
            }
            
            if (!isGrounded)
            {
                onLand?.Invoke();
                timer = onLandWait;
            }
        }
    }
}
