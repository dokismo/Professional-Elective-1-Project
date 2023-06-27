using System;
using System.Collections.Generic;
using Item.Gun;
using Item.Melee;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;
using SceneController;
namespace Player.Control
{
    public class PlayerStatus : MonoBehaviour
    {
        public static Action setItemEvent;
        public static Action heal;
        public delegate void Event(int amount);
        public static Event changeHealth;
        public static Event getMoney;
        
        public PlayerStatusScriptable playerStatusScriptable;
        public GameObject startGun1;
        public GameObject startGun2;

        public int maxGuns = 2;
        public int maxMedKit = 3;
        public int medKitHealAmount = 50;
        public float onTakeDamageInvulnerableTime = 1f;
        
        public List<GameObject> localWeapons;
        public int medKitCount;
        
        public Transform itemAnchor;
        
        private int currentIndex;
        private float takeDamageTimer;

        public Shooting CurrentWeapon { get; private set; }
        public Melee CurrentMelee { get; private set; }
        public bool Alive => playerStatusScriptable.health > 0;
        public Shooting PrimaryGun { get; private set; }
        public Melee PrimaryMelee { get; private set; }
        public Shooting SecondaryGun { get; private set; }
        public Melee SecondaryMelee { get; private set; }

        public bool GunInventoryIsFull => localWeapons.Count >= maxGuns;
        public bool MedKitInventoryIsFull => medKitCount >= maxMedKit;

        public static bool CanShoot => Cursor.lockState == CursorLockMode.Locked;


        private void OnEnable()
        {
            PlayerUIHandler.onMainMenu += DestroyPlayer;
            SceneManager.sceneLoaded += ResetPlayerStatsOnSceneLoad;
            PlayerUIHandler.onRetry += Retry;
            Enemy.OnDeathEvent.givePlayerMoney += playerStatusScriptable.AddMoney;
            changeHealth += OnTakeDamage;
            getMoney += playerStatusScriptable.PutMoney;
            EnemyHpHandler.OnTheDeath += KilledAZombie;
        }

        private void OnDisable()
        {
            PlayerUIHandler.onMainMenu -= DestroyPlayer;
            SceneManager.sceneLoaded -= ResetPlayerStatsOnSceneLoad;
            PlayerUIHandler.onRetry -= Retry;
            Enemy.OnDeathEvent.givePlayerMoney -= playerStatusScriptable.AddMoney;
            changeHealth -= OnTakeDamage;
            getMoney -= playerStatusScriptable.PutMoney;
            EnemyHpHandler.OnTheDeath -= KilledAZombie;
        }

        private void Start()
        {
            playerStatusScriptable.CharacterStatsMultiplier = InformationHolder.instance.CharacterMultiplier;
            GetComponent<Movement>().speed *= playerStatusScriptable.CharacterStatsMultiplier.SpeedMultiplier;
            GetComponent<Movement>().sprintingSpeed *= playerStatusScriptable.CharacterStatsMultiplier.SpeedMultiplier;
            playerStatusScriptable.money = 0;
            playerStatusScriptable.killCount = 0;
            
            playerStatusScriptable.SetPlayer(this);
            playerStatusScriptable.SetHealthBy((int)playerStatusScriptable.maxHealth);
            playerStatusScriptable.SetStaminaBy(playerStatusScriptable.maxStamina);

            if (Camera.main != null) 
                itemAnchor = Camera.main.gameObject.transform;

            if (startGun1 != null)
                AddWeapon(startGun1);
            if (startGun2 != null)
                AddWeapon(startGun2);
        }

        private void OnTakeDamage(int amount)
        {
            if (takeDamageTimer > 0) return;

            takeDamageTimer = onTakeDamageInvulnerableTime;
            playerStatusScriptable.SetHealthBy(amount);
        }

        private void KilledAZombie()
        {
            playerStatusScriptable.killCount++;
        }

        private void Update()
        {
            Selection();
            takeDamageTimer = Mathf.Clamp(takeDamageTimer - Time.deltaTime, 0, onTakeDamageInvulnerableTime);
        }
        
        public void Switch(int position = 0)
        {
            currentIndex = position < 0 ? currentIndex : position;

            CurrentWeapon = null;
            CurrentMelee = null;
            PrimaryGun = null;
            SecondaryGun = null;
            PrimaryMelee = null;
            SecondaryMelee = null;
            
            for (int i = 0; i < localWeapons.Count; i++)
            {
                if (currentIndex == i)
                {
                    setItemEvent?.Invoke();
                    
                    localWeapons[i].SetActive(true);
                    Shooting gun = localWeapons[i].GetComponent<Shooting>();
                    Melee melee = localWeapons[i].GetComponent<Melee>();
                    PrimaryMelee = melee;
                    PrimaryGun = gun;
                    CurrentWeapon = gun;
                    CurrentMelee = melee;
                }
                else
                {
                    localWeapons[i].SetActive(false);
                    SecondaryGun = localWeapons[i].GetComponent<Shooting>();
                    SecondaryMelee = localWeapons[i].GetComponent<Melee>();
                }
            }
        }
        
        public void AddWeapon(GameObject gun)
        {
            if (GunInventoryIsFull)
            {
                if (PrimaryGun == null && PrimaryMelee == null) return;

                Destroy(PrimaryGun != null ? PrimaryGun.gameObject : PrimaryMelee.gameObject);
                GameObject replacementGun = Instantiate(gun, itemAnchor);

                localWeapons[currentIndex] = replacementGun;
            }
            else
            {
                GameObject instanceGun = Instantiate(gun, itemAnchor);
                //instanceGun.transform.localPosition = Vector3.zero;
                instanceGun.SetActive(false);
                localWeapons.Add(instanceGun);
            }
            Switch(currentIndex);
        }

        public void RemoveGun(GameObject gun)
        {
            if (gun == null && !localWeapons.Contains(gun)) return;

            localWeapons.Remove(gun);
            Destroy(gun);
        }

        private void Selection()
        {
            if (Keyboard.current.qKey.wasPressedThisFrame) Switch(currentIndex == 0 ? 1 : 0);
            if (Keyboard.current.cKey.wasPressedThisFrame) UseMedKit();
        }

        private void UseMedKit()
        {
            if (medKitCount <= 0 || Math.Abs(playerStatusScriptable.health - playerStatusScriptable.maxHealth) < 0.1) return;

            medKitCount--;
            heal?.Invoke();
            playerStatusScriptable.SetHealthBy(medKitHealAmount);
        }

        public void AddMedKit()
        {
            medKitCount++;
        }

        public void Retry()
        {
            Cursor.lockState = CursorLockMode.Locked;
            playerStatusScriptable.health = playerStatusScriptable.maxHealth;

            Shooting[] ShootScripts = FindObjectsOfType<Shooting>();
            
            foreach(Shooting script in ShootScripts)
            {
                script.enabled = true;
            }

            foreach(GameObject gun in localWeapons)
            {
                Destroy(gun);
            }
            localWeapons.Clear();
            AddWeapon(startGun1);
        }

        void ResetPlayerStatsOnSceneLoad(Scene sceneName, LoadSceneMode mode)
        {
            playerStatusScriptable.health = playerStatusScriptable.maxHealth;
            playerStatusScriptable.killCount = 0;
        }

        public void DestroyPlayer()
        {
            Destroy(gameObject);
        }
    }
}