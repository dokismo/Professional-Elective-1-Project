using System;
using UnityEngine;
using Random = UnityEngine.Random;

namespace Player
{
    public class Recoil : MonoBehaviour
    {
        public float recoilZ = 0.1f;
        public float maxRecoil = 1f;
        
        public float maxTimer = 1f;
        public float decreaseMultiplier = 0.5f;
        public float appliedTime = 0.3f;
        
        private float appliedRecoil = 0;
        private float timer = 1;
        
        private void OnEnable()
        {
            Shooting.onShoot += ApplyRecoil;
        }
        private void OnDisable()
        {
            Shooting.onShoot -= ApplyRecoil;
        }

        private void Update()
        {
            timer = Mathf.Clamp01(timer + Time.deltaTime * decreaseMultiplier);
            appliedRecoil = Mathf.Lerp(appliedRecoil, 0, timer);
            
            transform.localRotation = Quaternion.Euler(0, 0, appliedRecoil);
        }

        private void ApplyRecoil()
        {
            timer = Mathf.Clamp(timer - appliedTime, -maxTimer, 1);
            appliedRecoil = Mathf.Clamp(appliedRecoil + Random.Range(-recoilZ, recoilZ), -maxRecoil, maxRecoil);
        }
    }
}