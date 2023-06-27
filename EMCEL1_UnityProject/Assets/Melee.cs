using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using Core;
using Item.Gun;
using Audio_Scripts;

namespace Item.Melee
{
    public class Melee : MonoBehaviour
    {
        [SerializeField] Animator animator;

        [SerializeField] float Damage;
        [SerializeField] float ImpactForce;
        [SerializeField] float MeleeDistance;

        [SerializeField] Camera cam;

        public Sprite icon;

        public LayerMask targetLayers;

        private ParticleEffect particleEffect;
        private void Start()
        {
            particleEffect = GetComponent<ParticleEffect>();
            cam = Camera.main;
        }
        // Update is called once per frame
        void Update()
        {
            Actions();
        }

        void Actions()
        {
            if (Mouse.current.leftButton.wasPressedThisFrame)
            {
                animator.SetTrigger("Attack");
            }
        }

        void DoMelee()
        {
            Vector2 mousePos = Mouse.current.position.ReadValue();

            Ray ray = cam.ScreenPointToRay(mousePos);

            Physics.Raycast(ray, out var raycastHit, MeleeDistance, targetLayers, QueryTriggerInteraction.Ignore);

            if(raycastHit.collider != null)
            {
                ITarget target = raycastHit.collider.GetComponent<ITarget>();

                SurfaceType surface = target != null
                            ? SurfaceType.Flesh
                            : SurfaceType.Wall;

                particleEffect.SpawnEffect(raycastHit.point, raycastHit.normal, surface, raycastHit.collider.transform);

                Rigidbody targetRb = raycastHit.collider.GetComponent<Rigidbody>();
                if (targetRb != null && raycastHit.collider.gameObject.layer == 8) targetRb.AddForce(raycastHit.point + (-raycastHit.normal * ImpactForce * Damage), ForceMode.Force);

                if (surface == SurfaceType.Wall)
                {
                    GlobalSfx.wallEvent?.Invoke(raycastHit.point);
                }
                if (surface == SurfaceType.Flesh)
                {
                    GlobalSfx.fleshEvent?.Invoke(raycastHit.point);
                }

                target?.Hit((int)Damage);
            }
        }
    }

}
