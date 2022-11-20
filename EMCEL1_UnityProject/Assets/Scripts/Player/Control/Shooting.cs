using UnityEngine;
using UnityEngine.InputSystem;
using Random = UnityEngine.Random;

namespace Player.Control
{
    public class Shooting : MonoBehaviour
    {
        public float distance;
        public LayerMask targetLayers;

        public int ammoInMag;
        public int ammoPerMag;
        public int totalAmmo;

        public float rpm = 60;
        public int ammoPerFire = 1;
        public float recoil;
        public float recoilControl = 2;

        public float reloadTime = 1;
        
        private float FireTime => 60 / rpm;
        private float currentRecoil = 0;
        private float fireTimer = 0;
        private float reloadTimer = 0;
        private bool reloadToggler = false;
        
        public bool CanShoot => !IsReloading && fireTimer <= 0 && ammoInMag > 0;
        public bool IsReloading => reloadTimer > 0;
        
        
        public InputAction fireInput;
        private Camera thisCamera;

        private void Start()
        {
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
            if (reloadToggler && !IsReloading)
            {
                reloadToggler = false;
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

            for (int i = 0; i < ammoPerFire; i++)
            {
                var randomCircle = Random.insideUnitCircle * Mathf.Clamp(currentRecoil, 1, 55);
                var precision = ReturnRandomPoint(randomCircle);
                
                Ray ray = thisCamera.ScreenPointToRay(precision);

                if (!Physics.Raycast(ray, out var raycastHit, distance, targetLayers)) continue;
                
                Debug.DrawLine(Camera.main.transform.position, raycastHit.point, Color.white, 2);
                // Debug.Log($"{raycastHit.point}");
                
                if (raycastHit.collider.GetComponent<GameObject>() != null)
                {
                    // Destroy(raycastHit.collider.gameObject);
                }

                //// ADDED BY AYNEY
                if(raycastHit.collider.gameObject.tag == "Enemy")
                {
                    float sampleDamage = 40f;
                    raycastHit.collider.gameObject.GetComponent<EnemyHpHandler>().takeDamage(sampleDamage);
                }
                ////
            }
            
            currentRecoil += recoil;
        }

        private void CheckForReload()
        {
            if (reloadToggler || IsReloading || ammoInMag > 0 || totalAmmo <= 0) return;
            
            reloadToggler = true;
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
