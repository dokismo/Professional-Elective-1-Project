using System;
using Player.Control;
using UnityEngine;

namespace Gun
{
    public class GunAnimation : MonoBehaviour
    {
        public float shootPauseTime = 0.3f;
        public Animator animator;
        
        private float timer;
        private Movement movement;
        
        private static readonly int Reload = Animator.StringToHash("Reload");
        private static readonly int ReloadDone = Animator.StringToHash("ReloadDone");
        private static readonly int Speed = Animator.StringToHash("Speed");

        private void Start()
        {
            movement = Movement.getMovement?.Invoke();
        }

        private void Update()
        {
            timer = Mathf.Clamp01(timer - Time.deltaTime);
            
            animator.SetFloat(Speed, movement.moveDir.magnitude);
            animator.enabled = timer <= 0;
        }

        public void ShootEvent(Vector3 point)
        {
            timer = shootPauseTime;
            
            transform.LookAt(point);
            
            float angleY = Mathf.Clamp(transform.localRotation.eulerAngles.y - 90, 270, 360);

            transform.localRotation = Quaternion.Euler(
                0,
                angleY, 
                Mathf.Clamp(transform.localRotation.eulerAngles.z, -4, 4));
        }

        public void ReloadEvent()
        {
            timer = 0;
            animator.SetTrigger(Reload);
        }
        public void ReloadDoneEvent()
        {
            timer = 0;
            animator.SetTrigger(ReloadDone);
        }
    }
}