using System.Collections.Generic;
using Gun;
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
        public List<GameObject> localGuns;
        public Transform gunAnchor;
        
        private int currentIndex = 0;
        
        public Shooting PrimaryGun { get; private set; }
        public Shooting SecondaryGun { get; private set; }
        public bool InventoryIsFull => localGuns.Count >= maxGuns;

        public static bool CanShoot => Cursor.lockState == CursorLockMode.Locked;

        private void OnEnable()
        {
            Enemy.OnDeathEvent.givePlayerMoney += playerStatusScriptable.AddMoney;
            changeHealth += playerStatusScriptable.SetHealthBy;
            getMoney += playerStatusScriptable.PutMoney;
            EnemyHpHandler.OnTheDeath += KilledAZombie;
        }

        private void OnDisable()
        {
            Enemy.OnDeathEvent.givePlayerMoney -= playerStatusScriptable.AddMoney;
            changeHealth -= playerStatusScriptable.SetHealthBy;
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
            {
                for (int i = 0; i < Camera.main.gameObject.transform.childCount; i++)
                {
                    Shooting shooting = Camera.main.gameObject.transform.GetChild(i).GetComponent<Shooting>();
                    
                    if (!shooting) continue;
                    
                    localGuns.Add(shooting.gameObject);
                }

                gunAnchor = Camera.main.gameObject.transform;
            }
            
            if (startGun1 != null)
                AddGun(startGun1);
            if (startGun2 != null)
                AddGun(startGun2);
        }

        private void KilledAZombie()
        {
            playerStatusScriptable.killCount++;
        }

        private void Update()
        {
            Selection();
            
            if (Keyboard.current.spaceKey.wasPressedThisFrame)
            {
                playerStatusScriptable.AddMoney(2000);
            }
        }
        
        public void Switch(int position = 0)
        {
            currentIndex = position < 0 ? currentIndex : position;
                
            PrimaryGun = null;
            SecondaryGun = null;
            
            for (int i = 0; i < localGuns.Count; i++)
            {
                if (currentIndex == i)
                {
                    localGuns[i].SetActive(true);
                    PrimaryGun = localGuns[i].GetComponent<Shooting>();
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
            if (Keyboard.current.digit1Key.wasPressedThisFrame)
            {
                Switch(0);
            }
            else if (Keyboard.current.digit2Key.wasPressedThisFrame)
            {
                Switch(1);
            }
        }
    }
}