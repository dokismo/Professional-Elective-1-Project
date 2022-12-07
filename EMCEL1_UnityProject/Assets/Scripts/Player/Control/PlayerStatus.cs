using System.Collections.Generic;
using Item.Gun;
using Item.MedKit;
using UnityEngine;
using UnityEngine.InputSystem;

namespace Player.Control
{
    public class PlayerStatus : MonoBehaviour
    {
        public delegate void Event(int amount);
        public static Event changeHealth;
        public static Event getMoney;
        
        public PlayerStatusScriptable playerStatusScriptable;
        public GameObject startGun1;
        public GameObject startGun2;

        public int maxGuns = 2;
        public int maxMedKit = 3;
        public float onTakeDamageInvulnerableTime = 1f;
        
        public List<GameObject> localGuns;
        public List<GameObject> localMeds;
        
        public Transform itemAnchor;
        
        private int currentIndex;
        private float takeDamageTimer;

        public Shooting CurrentGun { get; private set; }
        public bool Alive => playerStatusScriptable.health > 0;
        public Shooting PrimaryGun { get; private set; }
        public Shooting SecondaryGun { get; private set; }
        
        public bool GunInventoryIsFull => localGuns.Count >= maxGuns;
        public bool MedKitInventoryIsFull => localMeds.Count >= maxMedKit;

        public static bool CanShoot => Cursor.lockState == CursorLockMode.Locked;

        private void OnEnable()
        {
            Enemy.OnDeathEvent.givePlayerMoney += playerStatusScriptable.AddMoney;
            changeHealth += OnTakeDamage;
            getMoney += playerStatusScriptable.PutMoney;
            EnemyHpHandler.OnTheDeath += KilledAZombie;
            MedKit.itemHeal += UseMedKit;
        }

        private void OnDisable()
        {
            Enemy.OnDeathEvent.givePlayerMoney -= playerStatusScriptable.AddMoney;
            changeHealth -= OnTakeDamage;
            getMoney -= playerStatusScriptable.PutMoney;
            EnemyHpHandler.OnTheDeath -= KilledAZombie;
            MedKit.itemHeal -= UseMedKit;
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

            DisableEverything();
            
            CurrentGun = null;
            PrimaryGun = null;
            SecondaryGun = null;
            
            for (int i = 0; i < localGuns.Count; i++)
            {
                if (currentIndex == i)
                {
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

        private void DisableEverything()
        {
            foreach (var obj in localGuns)
            {
                obj.SetActive(false);
            }

            foreach (var obj in localMeds)
            {
                obj.SetActive(false);
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
            if (Keyboard.current.cKey.wasPressedThisFrame) GetMedKit();
        }

        private void GetMedKit()
        {
            if (localMeds.Count <= 0) return;
            
            DisableEverything();
            
            localMeds[0].SetActive(true);
        }

        public void AddMedKit(GameObject med)
        {
            if (MedKitInventoryIsFull) return;
            
            GameObject instanceMed = Instantiate(med, itemAnchor);
            instanceMed.SetActive(false);
            localMeds.Add(instanceMed);
        }
        
        private void UseMedKit()
        {
            Switch(currentIndex);
        }
    }
}