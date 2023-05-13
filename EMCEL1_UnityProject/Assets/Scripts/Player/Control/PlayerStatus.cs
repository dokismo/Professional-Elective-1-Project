using System;
using System.Collections.Generic;
using Item.Gun;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;
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
        
        public List<GameObject> localGuns;
        public int medKitCount;
        
        public Transform itemAnchor;
        
        private int currentIndex;
        private float takeDamageTimer;

        public Shooting CurrentGun { get; private set; }
        public bool Alive => playerStatusScriptable.health > 0;
        public Shooting PrimaryGun { get; private set; }
        public Shooting SecondaryGun { get; private set; }
        
        public bool GunInventoryIsFull => localGuns.Count >= maxGuns;
        public bool MedKitInventoryIsFull => medKitCount >= maxMedKit;

        public static bool CanShoot => Cursor.lockState == CursorLockMode.Locked;


        private void OnEnable()
        {
            SceneManager.sceneLoaded += ResetPlayerStatsOnSceneLoad;
            PlayerUIHandler.onRetry += Retry;
            Enemy.OnDeathEvent.givePlayerMoney += playerStatusScriptable.AddMoney;
            changeHealth += OnTakeDamage;
            getMoney += playerStatusScriptable.PutMoney;
            EnemyHpHandler.OnTheDeath += KilledAZombie;
        }

        private void OnDisable()
        {
            SceneManager.sceneLoaded -= ResetPlayerStatsOnSceneLoad;
            PlayerUIHandler.onRetry -= Retry;
            Enemy.OnDeathEvent.givePlayerMoney -= playerStatusScriptable.AddMoney;
            changeHealth -= OnTakeDamage;
            getMoney -= playerStatusScriptable.PutMoney;
            EnemyHpHandler.OnTheDeath -= KilledAZombie;
        }

        private void Start()
        {
            playerStatusScriptable.money = 0;
            playerStatusScriptable.killCount = 0;
            
            playerStatusScriptable.SetPlayer(this);
            playerStatusScriptable.SetHealthBy((int)playerStatusScriptable.maxHealth);
            playerStatusScriptable.SetStaminaBy(playerStatusScriptable.maxStamina);

            if (Camera.main != null) 
                itemAnchor = Camera.main.gameObject.transform;

            if (startGun1 != null)
                AddGun(startGun1);
            if (startGun2 != null)
                AddGun(startGun2);
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

            CurrentGun = null;
            PrimaryGun = null;
            SecondaryGun = null;
            
            for (int i = 0; i < localGuns.Count; i++)
            {
                if (currentIndex == i)
                {
                    setItemEvent?.Invoke();
                    
                    localGuns[i].SetActive(true);
                    Shooting gun = localGuns[i].GetComponent<Shooting>();
                    PrimaryGun = gun;
                    CurrentGun = gun;
                }
                else
                {
                    localGuns[i].SetActive(false);
                    SecondaryGun = localGuns[i].GetComponent<Shooting>();
                }
            }
        }
        
        public void AddGun(GameObject gun)
        {
            if (GunInventoryIsFull)
            {
                if (PrimaryGun == null) return;

                Destroy(PrimaryGun.gameObject);
                GameObject replacementGun = Instantiate(gun, itemAnchor);

                localGuns[currentIndex] = replacementGun;
            }
            else
            {
                GameObject instanceGun = Instantiate(gun, itemAnchor);
                instanceGun.SetActive(false);
                localGuns.Add(instanceGun);
            }
            Switch(currentIndex);
        }

        public void RemoveGun(GameObject gun)
        {
            if (gun == null && !localGuns.Contains(gun)) return;

            localGuns.Remove(gun);
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
        }

        void ResetPlayerStatsOnSceneLoad(Scene sceneName, LoadSceneMode mode)
        {
            playerStatusScriptable.health = playerStatusScriptable.maxHealth;
            playerStatusScriptable.killCount = 0;
        }
    }
}