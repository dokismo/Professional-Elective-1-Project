using System;
using UnityEngine;

namespace Player.Control
{
    [RequireComponent(typeof(Rigidbody))]
    public class Bullet : MonoBehaviour
    {
        public float lifeTime;
        public float speed = 20;

        public Rigidbody rb;
        public LayerMask targetLayers;

        private void Start()
        {
            Destroy(gameObject, lifeTime);
        }

        private void FixedUpdate()
        {
            rb.AddForce(-transform.forward * (speed * Time.fixedDeltaTime));
        }

        private void OnTriggerExit(Collider other)
        {
            if (other.gameObject.layer != targetLayers) return;

            Destroy(gameObject);
        }
    }
}