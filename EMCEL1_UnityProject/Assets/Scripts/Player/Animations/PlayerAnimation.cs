using _2._5D_Objects;
using Player.Control;
using UnityEngine;

namespace Player.Animations
{
    public class PlayerAnimation : MonoBehaviour
    {
        public Movement movement;
        public Transform orientation, cameraAnchor;
        public Animator animator;
        public SpriteRotation spriteRotation;
    
        private static readonly int Speed = Animator.StringToHash("Speed");
        private static readonly int Front = Animator.StringToHash("Front");
        private static readonly int Side = Animator.StringToHash("Side");
        private static readonly int Jump = Animator.StringToHash("Jump");
        private static readonly int Land = Animator.StringToHash("OnLand");
        private static readonly int Falling = Animator.StringToHash("Falling");
        private static readonly int Climb = Animator.StringToHash("OnClimb");

        private void OnEnable()
        {
            Movement.OnJump += OnJump;
            Movement.OnLand += OnLand;
            Movement.OnClimb += OnClimb;
        }

        private void OnDisable()
        {
            Movement.OnJump -= OnJump;
            Movement.OnLand -= OnLand;
            Movement.OnClimb -= OnClimb;
        }

        private void Update()
        {
            CorrectSpriteForward();
            SetDirections();

            float moveMagnitude = movement.moveDir.magnitude;
            animator.SetFloat(Speed, moveMagnitude);
            animator.SetBool(Falling, movement.IsFalling);
        }

        private void OnClimb() => animator.SetTrigger(Climb); 
        private void OnJump() => animator.SetTrigger(Jump);
        private void OnLand() => animator.SetTrigger(Land);

        private void SetDirections()
        {
            animator.SetFloat(Front, spriteRotation.Front);
            animator.SetFloat(Side, spriteRotation.Side);
        }

        private void CorrectSpriteForward()
        {
            if (!movement.IsMoving || !movement.CanMove) return;
        
            orientation.LookAt(orientation.position + movement.moveDir);
        }
    }
}
