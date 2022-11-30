using System;
using UnityEngine;

namespace Gun
{
    public class GunAnimation : MonoBehaviour
    {
        public float shootPauseTime = 0.3f;
        public Animator animator;
        
        private float timer;
        private float reloadTimer;
        private static readonly int Reload1 = Animator.StringToHash("Reload");

        private void Update()
        {
            timer = Mathf.Clamp01(timer - Time.deltaTime);
            reloadTimer = Mathf.Clamp(reloadTimer - Time.deltaTime, 0, 3);
            
            animator.SetBool(Reload1, reloadTimer > 0);
            animator.enabled = (timer <= 0 && reloadTimer <= 0) || reloadTimer > 0;
        }

        public void ShootEvent(Vector3 point)
        {
            timer = shootPauseTime;
            
            transform.LookAt(point);
            transform.localRotation = Quaternion.Euler(
                0,
                -90, 
                Mathf.Clamp(transform.localRotation.eulerAngles.z, -4, 4));
        }

        public void Reload(float time)
        {
            reloadTimer = time;
        }
    }
}