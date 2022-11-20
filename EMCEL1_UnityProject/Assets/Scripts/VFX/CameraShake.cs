using System;
using UnityEngine;
using Random = UnityEngine.Random;

namespace VFX
{
    public class CameraShake : MonoBehaviour
    {
        public AnimationCurve curve;
        public float duration = 1f;

        public delegate void Shake();
        public static Shake shakeOnce;

        private float elapsedTime;
        private bool IsShaking => elapsedTime >= duration;
        private Vector3 startPosition;

        private void Start()
        {
            elapsedTime = 99;
            startPosition = transform.localPosition;
        }

        private void OnEnable()
        {
            shakeOnce += StartShake;
        }

        private void OnDisable()
        {
            shakeOnce -= StartShake;
        }

        private void Update()
        {
            Shaker();
        }

        private void Shaker()
        {
            if (!IsShaking) return;
            
            while (elapsedTime < duration)
            {
                elapsedTime += Time.deltaTime;
                float strength = curve.Evaluate(elapsedTime / duration);
                transform.localPosition = startPosition + Random.insideUnitSphere * strength;
                return;
            }
            
            transform.localPosition = startPosition;
        }

        private void StartShake() => elapsedTime = duration;
    }
}