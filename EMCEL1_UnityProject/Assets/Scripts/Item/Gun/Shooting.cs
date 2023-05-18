using Audio_Scripts;
using Core;
using Gun;
using Player.Control;
using UI.PlayerScreen;
using UnityEngine;
using UnityEngine.InputSystem;
using SceneController;
using Random = UnityEngine.Random;

namespace Item.Gun
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
        public string gunName = "";
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
        public Sprite icon;
        public MuzzleFlash muzzleFlash;
       

        private float swapTimer;
        private bool able;
        public float FireTime => 60f / rpm;
        private float fireTimer;
        private float reloadTimer;
        private bool reloadToggle;

        public bool CanShoot => !IsReloading && fireTimer <= 0 && ammoInMag > 0 && able;
        public bool IsReloading => reloadTimer > 0;
        public bool CanBuyAmmo => totalAmmo < maxTotalAmmo;

        private GunAnimation gunAnimation;
        private FirePath firePath;
        private Camera thisCamera;
        private ParticleEffect particleEffect;
        private GunSound gunSound; //SFX
        private GlobalSfx surfaceSound;

        private int didntFried;
        
        private void Start()
        {
            damage = (int)(damage * InformationHolder.instance.CharacterMultiplier.AttackDamageMultiplier);
            particleEffect = GetComponent<ParticleEffect>();
            thisCamera = Camera.main;

            gunAnimation ??= GetComponent<GunAnimation>();
            firePath = GetComponent<FirePath>();
        }

        private void OnEnable()
        {
            PlayerUIHandler.onRetry += RefillAmmo;

            gunAnimation ??= GetComponent<GunAnimation>();
            
            RecoilEffect.setControl?.Invoke(recoilControl);
            
            muzzleFlash.gameObject.SetActive(false);
            swapTimer = 0.5f;
            able = false;
            gunAnimation.SwapEvent();

            DisplayStatus.onDead += PlayerDeath;
        }

        private void OnDisable()
        {
            
            StopReload();
            DisplayStatus.onDead -= PlayerDeath;
        }

        private void PlayerDeath()
        {
            enabled = false;
        }

        private void Update()
        {
            ReloadToggler();
            Timer();
            Actions();
        }

        private void Actions()
        {
            if (!able)
            {
                if (swapTimer <= 0)
                {
                    able = true;
                    gunAnimation.SwapDoneEvent();
                }
                return;
            }

            if (Mouse.current.leftButton.isPressed && operation == Operation.Automatic)
                Fire();
            
                
            else if (operation == Operation.SemiAutomatic && Mouse.current.leftButton.wasPressedThisFrame)
                Fire();

            if (Keyboard.current.rKey.wasPressedThisFrame) Reload();
        }

        public void StopReload()
        {
            if (!IsReloading || !reloadToggle) return;
            
            reloadToggle = false;
            reloadTimer = 0;
        }

        private void ReloadToggler()
        {
            if (reloadToggle && !IsReloading)
            {
                reloadToggle = false;
                ReloadMagazine();
            }
        }

        private void Timer()
        {
            swapTimer = Mathf.Clamp01(swapTimer - Time.deltaTime);
            fireTimer = Mathf.Clamp(fireTimer - Time.deltaTime, 0, 99);
            reloadTimer = Mathf.Clamp(reloadTimer - Time.deltaTime, 0, 99);
        }


        private void Fire()
        {
            CheckForReload();

            if (!PlayerStatus.CanShoot || !CanShoot || IsReloading) return;

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


            GunSound.shootEvent?.Invoke();
            muzzleFlash.gameObject.SetActive(true);
            GunLight.triggerLight?.Invoke();
            
            for (didntFried++; didntFried > 0; didntFried--)
            for (int i = 0; i < ammoPerFire; i++)
            {
                

                Vector2 mousePos = Mouse.current.position.ReadValue();
                Vector2 gotRecoil = new Vector2(0, RecoilEffect.apply?.Invoke(recoil, maxRecoil) ?? 0);
                
                Ray ray = thisCamera.ScreenPointToRay(mousePos + 
                                                      (fireType == FireType.Linear 
                                                      ? gotRecoil
                                                      : Random.insideUnitCircle * recoil));

                Physics.Raycast(ray, out var raycastHit, distance, targetLayers, QueryTriggerInteraction.Ignore);
                    
                if (raycastHit.collider != null)
                {
                    ITarget target = raycastHit.collider.GetComponent<ITarget>();
                    
                    SurfaceType surface = target != null
                                ? SurfaceType.Flesh
                                : SurfaceType.Wall;
                    
                    particleEffect.SpawnEffect(raycastHit.point, raycastHit.normal, surface);
                    
                    if (surface == SurfaceType.Wall)
                    {
                        GlobalSfx.wallEvent?.Invoke(raycastHit.point);
                    }
                    if (surface == SurfaceType.Flesh)
                    {
                        GlobalSfx.fleshEvent?.Invoke(raycastHit.point);
                    }

                    target?.Hit(damage);

                    gunAnimation.ShootEvent(raycastHit.point);
                    firePath.RenderLine(raycastHit.point);
                }
            }
        }

        private void CheckForReload()
        {
            if (reloadToggle || ammoInMag > 0) return;

            Reload();
        }

        public void RefillAmmo()
        {
            ammoInMag = ammoPerMag;
            totalAmmo = maxTotalAmmo;
            Debug.Log("CALLED REFILL");
        }

        private void Reload()
        {
            if (totalAmmo <= 0 || ammoInMag == ammoPerMag || IsReloading) return;

            GunSound.reloadEvent?.Invoke();
            gunAnimation.ReloadEvent();
            reloadToggle = true;
            reloadTimer += reloadTime;
        }

        private void ReloadMagazine()
        {
            if (!able) return;
            
            gunAnimation.ReloadDoneEvent();
            int neededAmmo = ammoPerMag - ammoInMag;
            int gotAmount 
                = totalAmmo <= neededAmmo 
                ? totalAmmo 
                : Mathf.Clamp(neededAmmo, 0, ammoPerMag);
            
            totalAmmo -= gotAmount;
            ammoInMag += gotAmount;
        }

        private void OnDestroy()
        {
            PlayerUIHandler.onRetry -= RefillAmmo;
        }
    }
}
