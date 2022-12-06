using System.Collections.Generic;
using Gun;
using UI;
using UI.PlayerScreen;
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

        public int maxGuns = 3;
        public float onTakeDamageInvulnerableTime = 1f;
        public List<GameObject> localGuns;
        public Transform gunAnchor;
        
        private int currentIndex;
        private float takeDamageTimer;

        public Shooting CurrentGun { get; private set; }
        public bool Alive => playerStatusScriptable.health > 0;
        public Shooting PrimaryGun { get; private set; }
        public Shooting SecondaryGun { get; private set; }
        
        public bool InventoryIsFull => localGuns.Count >= maxGuns;

        public static bool CanShoot => Cursor.lockState == CursorLockMode.Locked;

        private void OnEnable()
        {
            Enemy.OnDeathEvent.givePlayerMoney += playerStatusScriptable.AddMoney;
            changeHealth += OnTakeDamage;
            getMoney += playerStatusScriptable.PutMoney;
            EnemyHpHandler.OnTheDeath += KilledAZombie;
        }

        private void OnDisable()
        {
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
            
            gunAnchor = Camera.main.gameObject.transform;
            
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
            if (InventoryIsFull)
            {
                if (PrimaryGun == null) return;

                Destroy(PrimaryGun.gameObject);
                GameObject replacementGun = Instantiate(gun, gunAnchor);

                localGuns[currentIndex] = replacementGun;
            }
            else
            {
                GameObject instanceGun = Instantiate(gun, gunAnchor);
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
        }
    }
}