using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.Serialization;

namespace Player.Control
{
    [RequireComponent(typeof(CharacterController))]
    public class Movement : MonoBehaviour
    {
        public delegate void OnAction();
        public static OnAction OnJump;
        public static OnAction OnLand;
        public static OnAction OnClimb;
        
        [Header("Spec")]
        public float speed = 8f;
        public float gravity = -9.81f;
        public float jumpHeight = 3f;
        public float onLandWait = 0.6f;
        
        [Header("Setting")]
        public Transform groundCheck;
        public Transform wallCheck, cameraAnchor;
        public float groundDistance = 0.4f, wallDistance = 0.5f;
        public LayerMask groundLayer;
        [FormerlySerializedAs("inputAction")] public InputAction movementInput;
        public InputAction climbing;
    
        [HideInInspector] public Vector3 moveDir;
        [HideInInspector] public Vector3 velocity;
        [HideInInspector] public bool isClimbing;
        private CharacterController _controller;
        [HideInInspector] public bool isGrounded;
        private float _timer;

        public bool IsClimbing { get; private set; }
        public bool CanMove => _timer <= 0;
        public bool IsMoving => moveDir.magnitude > 0.1f;
        public bool IsFalling => velocity.y < -0.02f;

        private void OnEnable()
        {
            movementInput.Enable();
            climbing.Enable();
            //climbing.started += CheckWall;
        }

        private void OnDisable()
        {
            movementInput.Disable();
            //climbing.started -= CheckWall;
            climbing.Disable();
        }

        private void Start()
        {
            _timer = 0f;
            _controller = GetComponent<CharacterController>();
        }

        private void OnDrawGizmosSelected()
        {
            Gizmos.DrawWireSphere(groundCheck.position, groundDistance);
            Gizmos.DrawWireSphere(wallCheck.position, wallDistance);
        }

        private void Update()
        {
            _timer = Mathf.Clamp(_timer - Time.deltaTime, 0, 20);
            Vector3 inputs = movementInput.ReadValue<Vector3>();
            
            moveDir =
                (cameraAnchor ? cameraAnchor.right : transform.right) * inputs.x +
                (cameraAnchor ? cameraAnchor.forward : transform.forward) * inputs.z;

            bool checkFloor = Physics.CheckSphere(groundCheck.position, groundDistance, groundLayer);
            
            CheckFloor(checkFloor, inputs);
            
            isGrounded = checkFloor;
        }

        private void FixedUpdate()
        {
            velocity.y += gravity * Time.fixedDeltaTime;

            Vector3 movementDirection = Vector3.zero;
            if (isClimbing)
                movementDirection = new Vector3(moveDir.x, moveDir.z, 0).normalized;
            else if (CanMove)
                movementDirection = moveDir.normalized;
            
            _controller.Move((!isClimbing? velocity : Vector3.zero) + movementDirection * (speed * Time.fixedDeltaTime));
        }

        private void CheckFloor(bool checkFloor, Vector3 inputs)
        {
            if (!checkFloor) return;
                
            if (velocity.y < 0)
                velocity.y = -0.01f;

            if (inputs.y > 0 && CanMove)
            {
                velocity.y = Mathf.Sqrt(jumpHeight * -2f * gravity);
                OnJump?.Invoke();
            }
            
            if (!isGrounded)
            {
                OnLand?.Invoke();
                _timer = onLandWait;
            }
        }

        private void CheckWall(InputAction.CallbackContext ctx)
        {
            bool checkWall = Physics.CheckSphere(wallCheck.position, wallDistance, groundLayer);
        
            Debug.Log($"{IsClimbing}, {checkWall}, {ctx.started}");
            if (!checkWall || !ctx.started) return;
            
            IsClimbing = true;
            OnClimb?.Invoke();
        }
    }
}
