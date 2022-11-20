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

        [Header("Gun Specs")] public int damage = 20;
        public int
            ammoInMag,
            ammoPerMag,
            totalAmmo,
            rpm = 60,
            ammoPerFire = 1,
            recoil,
            recoilControl = 2,
            maxRecoil = 45,
            reloadTime = 1;

        private float FireTime => 60f / rpm;
        private float currentRecoil;
        private float fireTimer;
        private float reloadTimer;
        private bool reloadToggle;
        
        public bool CanShoot => !IsReloading && fireTimer <= 0 && ammoInMag > 0;
        public bool IsReloading => reloadTimer > 0;
        
        
        public InputAction fireInput;
        private Camera thisCamera;
        private ParticleEffect particleEffect;
        private int didntFried;

        private void Start()
        {
            particleEffect = GetComponent<ParticleEffect>();
            thisCamera = Camera.main;
        }

        private void OnEnable()
        {
            fireInput.Enable();
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
            currentRecoil = fireTimer <= 0 ? Mathf.Clamp(currentRecoil - recoilControl * Time.deltaTime, 0, 99) : currentRecoil;
            
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
                var randomCircle = Random.insideUnitCircle * Mathf.Clamp(currentRecoil, 1, maxRecoil);
                var precision = ReturnRandomPoint(randomCircle);

                Ray ray = thisCamera.ScreenPointToRay(precision);
                currentRecoil = Mathf.Clamp(currentRecoil + recoil, 0, maxRecoil);

                if (!Physics.Raycast(ray, out var raycastHit, distance, targetLayers)) continue;

                Debug.DrawLine(Camera.main.transform.position, raycastHit.point, Color.white, 2);

                ITarget target = raycastHit.collider.GetComponent<ITarget>();
                particleEffect.SpawnEffect(raycastHit.point, raycastHit.normal);
                target?.Hit(damage);
            }
            
            CameraShake.shakeOnce?.Invoke();
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
