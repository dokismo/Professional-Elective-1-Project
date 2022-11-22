using Core;
using Player.Control;
using UnityEngine;
using UnityEngine.InputSystem;
using VFX;
using Random = UnityEngine.Random;

namespace Gun
{
    public class Shooting : MonoBehaviour
    {
        public float distance;
        public LayerMask targetLayers;

        [Header("Gun Specs")] 
        public int damage = 20;
        public int
            ammoInMag,
            ammoPerMag,
            totalAmmo,
            rpm = 60,
            ammoPerFire = 1,
            reloadTime = 1,
            recoil,
            maxRecoil,
            recoilControl;

        private float FireTime => 60f / rpm;
        private float fireTimer;
        private float reloadTimer;
        private bool reloadToggle;


        public bool CanShoot => !IsReloading && fireTimer <= 0 && ammoInMag > 0;
        public bool IsReloading => reloadTimer > 0;
        
        
        public InputAction fireInput;
        private Camera thisCamera;
        private AudioSource sfx; //SFX
        private ParticleEffect particleEffect;
        private int didntFried;
        
        private void Start()
        {
            sfx = GetComponent<AudioSource>(); //SFX
            particleEffect = GetComponent<ParticleEffect>();
            thisCamera = Camera.main;
        }

        private void OnEnable()
        {
            fireInput.Enable();
            RecoilEffect.setControl?.Invoke(recoilControl);
        }

        private void OnDisable()
        {
            fireInput.Disable();
        }

        private void Update()
        {
            if (reloadToggle && !IsReloading)
            {
                reloadToggle = false;
                Reload();
            }
            
            fireTimer = Mathf.Clamp(fireTimer - Time.deltaTime, 0, 99);
            reloadTimer = Mathf.Clamp(reloadTimer - Time.deltaTime, 0, 99);
            
            if (Mouse.current.leftButton.isPressed)
            {
                Fire();
            }
        }


        private void Fire()
        {
            CheckForReload();

            if (!PlayerStatus.canShoot || !CanShoot) return;

            fireTimer += FireTime;
            ammoInMag -= ammoPerFire;

            if (FireTime < Time.deltaTime)
            {
                float excessFire = Time.deltaTime / FireTime;
                didntFried += Mathf.RoundToInt(excessFire);
                fireTimer += Time.deltaTime % FireTime;
            }
            for (didntFried++; didntFried > 0; didntFried--)
            for (int i = 0; i < ammoPerFire; i++)
            {
                Vector2 mousePos = Mouse.current.position.ReadValue();
                Vector2 gotRecoil = new Vector2(0, RecoilEffect.apply?.Invoke(recoil, maxRecoil) ?? 0);
                
                Ray ray = thisCamera.ScreenPointToRay(mousePos + gotRecoil);

                
                if (!Physics.Raycast(ray, out var raycastHit, distance, targetLayers)) continue;
                fireSfx();

                ITarget target = raycastHit.collider.GetComponent<ITarget>();
                particleEffect.SpawnEffect(raycastHit.point, raycastHit.normal, 
                    target != null 
                        ? SurfaceType.Flesh
                        : SurfaceType.Wall);
                
                target?.Hit(damage);
            }
            
            // CameraShake.shakeOnce?.Invoke();
        }

        private void fireSfx()
        {
            sfx.PlayOneShot(sfx.clip); //SFX
        }

        private void CheckForReload()
        {
            if (reloadToggle || IsReloading || ammoInMag > 0 || totalAmmo <= 0) return;
            
            reloadToggle = true;
            reloadTimer += reloadTime;
        }

        private void Reload()
        {
            int neededAmmo = ammoPerMag - ammoInMag;
            int gotAmount = totalAmmo <= neededAmmo ? totalAmmo : Mathf.Clamp(totalAmmo, 0, ammoPerMag);
            Debug.Log($"{neededAmmo} {gotAmount}");
            totalAmmo -= gotAmount;
            ammoInMag += gotAmount;
        }

        private Vector2 ReturnRandomPoint(Vector2 circlePoint)
        {
            var mousePosition = Mouse.current.position.ReadValue();
            return new Vector2(mousePosition.x + circlePoint.x, mousePosition.y + circlePoint.y);
        }
    }
}
