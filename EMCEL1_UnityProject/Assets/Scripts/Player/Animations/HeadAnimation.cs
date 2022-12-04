using System;
using Player.Control;
using UnityEngine;

namespace Player.Animations
{
    public class HeadAnimation : MonoBehaviour
    {
        public Animator recoilAnimator;

        private Movement movement;
        private static readonly int Speed = Animator.StringToHash("Speed");
        private void Start()
        {
            movement = GetComponent<Movement>();
        }

        private void Update()
        {
            recoilAnimator.SetFloat(Speed, movement.moveDir.magnitude);

            recoilAnimator.speed = movement.IsRunning 
                ? Mathf.Lerp(movement.speed, 2, movement.sprintingSpeed)
                : 1;
        }
    }
}