using Core;
using Player.Control;
using UnityEngine;
using UnityEngine.InputSystem;

namespace Gun
{
    public enum FireType
    {
        Linear,
        RandomCircle
    }

    public enum Operation
    {
        Automatic,
        SemiAutomatic
    }


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
            maxTotalAmmo,
            rpm = 60,
            ammoPerFire = 1,
            reloadTime = 1,
            recoil,
            maxRecoil,
            recoilControl;

        public FireType fireType = FireType.Linear;
        public Operation operation = Operation.Automatic;
        

        private float FireTime => 60f / rpm;
        private float fireTimer;
        private float reloadTimer;
        private bool reloadToggle;

        public bool CanShoot => !IsReloading && fireTimer <= 0 && ammoInMag > 0;
        public bool IsReloading => reloadTimer > 0;
        

        private GunAnimation gunAnimation;
        private GunLight gunLight;
        private FirePath firePath;
        private Camera thisCamera;
        private AudioSource sfx; //SFX
        private ParticleEffect particleEffect;
        private int didntFried;
        
        private void Start()
        {
            sfx = GetComponent<AudioSource>(); //SFX
            particleEffect = GetComponent<ParticleEffect>();
            thisCamera = Camera.main;

            gunAnimation = GetComponent<GunAnimation>();
            gunLight = GetComponent<GunLight>();
            firePath = GetComponent<FirePath>();
        }

        private void OnEnable()
        {
            RecoilEffect.setControl?.Invoke(recoilControl);
        }

        private void Update()
        {
            if (reloadToggle && !IsReloading)
            {
                reloadToggle = false;
                ReloadMagazine();
            }
            
            fireTimer = Mathf.Clamp(fireTimer - Time.deltaTime, 0, 99);
            reloadTimer = Mathf.Clamp(reloadTimer - Time.deltaTime, 0, 99);
            
            if (Mouse.current.leftButton.isPressed)
                if (operation == Operation.SemiAutomatic && Mouse.current.leftButton.wasPressedThisFrame)
                    Fire();
                else
                    Fire();

            if (Keyboard.current.rKey.wasPressedThisFrame) Reload();
        }


        private void Fire()
        {
            CheckForReload();

            if (!PlayerStatus.CanShoot || !CanShoot) return;

            fireTimer += FireTime;
            ammoInMag -= 
                fireType == FireType.Linear
                    ? ammoPerFire
                    : 1;

            if (FireTime < Time.deltaTime)
            {
                float excessFire = Time.deltaTime / FireTime;
                didntFried += Mathf.RoundToInt(excessFire);
                fireTimer += Time.deltaTime % FireTime;
            }
            
            for (didntFried++; didntFried > 0; didntFried--)
            for (int i = 0; i < ammoPerFire; i++)
            {
                fireSfx();
                gunLight.Light();

                Vector2 mousePos = Mouse.current.position.ReadValue();
                Vector2 gotRecoil = new Vector2(0, RecoilEffect.apply?.Invoke(recoil, maxRecoil) ?? 0);
                
                Ray ray = thisCamera.ScreenPointToRay(mousePos + 
                                                      (fireType == FireType.Linear 
                                                      ? gotRecoil
                                                      : Random.insideUnitCircle * recoil));

                Physics.Raycast(ray, out var raycastHit, distance, targetLayers);
                    
                if (raycastHit.collider != null)
                {
                    ITarget target = raycastHit.collider.GetComponent<ITarget>();
                    particleEffect.SpawnEffect(raycastHit.point, raycastHit.normal, 
                        target != null 
                            ? SurfaceType.Flesh
                            : SurfaceType.Wall);
                
                    target?.Hit(damage);

                    gunAnimation.ShootEvent(raycastHit.point);
                    firePath.RenderLine(raycastHit.point);
                }
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
            
            gunAnimation.Reload(reloadTime);
            reloadToggle = true;
            reloadTimer += reloadTime;
        }

        public void RefillAmmo()
        {
            ammoInMag = ammoPerMag;
            totalAmmo = maxTotalAmmo;
        }

        private void Reload()
        {
            if (totalAmmo <= 0 || ammoInMag == ammoPerMag) return;

            gunAnimation.Reload(reloadTime);
            reloadToggle = true;
            reloadTimer += reloadTime;
        }

        private void ReloadMagazine()
        {
            int neededAmmo = ammoPerMag - ammoInMag;
            int gotAmount 
                = totalAmmo <= neededAmmo 
                ? totalAmmo 
                : Mathf.Clamp(neededAmmo, 0, ammoPerMag);
            
            totalAmmo -= gotAmount;
            ammoInMag += gotAmount;
        }
    }
}
