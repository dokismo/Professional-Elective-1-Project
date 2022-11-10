using Player.Inventory;
using UnityEngine;

namespace Player
{
    public class Shooting : MonoBehaviour
    {
        public delegate void Shoot();
        public static Shoot onShoot;

        public AmmoType ammoType = AmmoType.Rifle;
    
        [Header("References")]
        [SerializeField] private Transform firePoint;
        [SerializeField] private GameObject bulletPrefab;
    
        [Header("Bullet Settings")]
        [SerializeField] private float bulletForce = 20f;

        [Header("Ammo")] 
        [SerializeField] private int totalAmmo = 90;
        [SerializeField] private int
            ammoInMagazine = 30,
            maxMagazineAmmo = 30;
        [SerializeField] private float reloadTime = 1.5f;
    
        [Header("Gun Settings")]
        [SerializeField] private bool isAutomatic;
        [SerializeField] private int bulletsPerFire = 1;
        [SerializeField] private float fireRate = 0.2f;
    
    
        public bool CanFire => fireRateTimer == 0 && reloadTimer == 0 && HasAmmoInMag;
        public bool HasAmmoInMag => ammoInMagazine > 0;
        public bool CanReload => !IsReloading && totalAmmo > 0 && ammoInMagazine < maxMagazineAmmo;
        public bool IsReloading => reloadTimer > 0;

        private float reloadTimer = 0;
        private float fireRateTimer = 0;

        private bool reloadToggler;

        void Update()
        {
            SetTimers();
            CheckFire();
        
            if (Input.GetKeyDown(KeyCode.R)) Reload();

            if (reloadToggler && !IsReloading)
            {
                reloadToggler = false;
                int localTotalAmmo = totalAmmo;
                totalAmmo = Mathf.Clamp(totalAmmo - maxMagazineAmmo, 0, 9999);

                ammoInMagazine = totalAmmo == 0 ? localTotalAmmo : maxMagazineAmmo;
            }
        }

        private void Reload()
        {
            if (!CanReload) return;
        
            reloadTimer = reloadTime;
            reloadToggler = true;
        }
    
        private void SetTimers()
        {
            fireRateTimer = SetTimer(fireRateTimer);
            reloadTimer = SetTimer(reloadTimer);
        }

        private float SetTimer(float value)
        {
            return Mathf.Clamp(value - Time.deltaTime, 0, 99);
        }

        private void CheckFire()
        {
            if (!CanFire) return;

            if (isAutomatic && Input.GetButton("Fire1"))
                Fire();
            else if (Input.GetButtonDown("Fire1")) 
                Fire();
        }

        private void Fire()
        {
            fireRateTimer = fireRate;

            int fireAmount = AmmoExpense();

            for (int i = 0; i < fireAmount; i++)
            {
                onShoot?.Invoke();
                
                GameObject bullet = Instantiate(bulletPrefab, firePoint.position, firePoint.rotation);
                Rigidbody2D rb = bullet.GetComponent<Rigidbody2D>();
                Vector2 bulletPos = new Vector2(bullet.transform.position.x, bullet.transform.position.y);

                rb.AddForce(firePoint.right * bulletForce, ForceMode2D.Impulse);
            }
            
        
            if (ammoInMagazine == 0)
                Reload();
        }

        private int AmmoExpense()
        {
            ammoInMagazine -= bulletsPerFire;

            if (ammoInMagazine < 0)
                return bulletsPerFire - ammoInMagazine;

            return bulletsPerFire;
        }

        public void AddAmmo(int amount)
        {
            totalAmmo += amount;
        }
    }
}
