using System;
using UnityEngine;

namespace Gun
{
    public class GunLight : MonoBehaviour
    {
        public Light fireLight;

        public float lifeTime = 0.05f;

        private float timer;

        private void Start()
        {
            fireLight.enabled = false;
        }

        private void Update()
        {
            timer = Mathf.Clamp01(timer - Time.deltaTime);
            fireLight.enabled = timer > 0;
        }

        public void Light() => timer = lifeTime;
    }
}