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
        public GameObject ak47;
        public GameObject m4;

        public int maxGuns = 3;
        public List<GameObject> localGuns;
        public Transform gunAnchor;
        
        private int currentIndex = 0;
        
        public Shooting CurrentGun { get; private set; }
        public bool InventoryIsFull => localGuns.Count >= maxGuns;

        public static bool CanShoot => Cursor.lockState == CursorLockMode.Locked;

        private void OnEnable()
        {
            Enemy.OnDeathEvent.givePlayerMoney += playerStatusScriptable.AddMoney;
            changeHealth += playerStatusScriptable.SetHealthBy;
            getMoney += playerStatusScriptable.PutMoney;
        }

        private void OnDisable()
        {
            Enemy.OnDeathEvent.givePlayerMoney -= playerStatusScriptable.AddMoney;
            changeHealth -= playerStatusScriptable.SetHealthBy;
            getMoney -= playerStatusScriptable.PutMoney;
        }
        
        private void Start()
        {
            Cursor.lockState = CursorLockMode.Locked;
            
            playerStatusScriptable.SetPlayer(this);
            
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
            
            AddGun(ak47);
            AddGun(m4);
            
            Switch(0);
        }

        private void Update()
        {
            Selection();
        }
        
        public void Switch(int position = -1)
        {
            currentIndex = position < 0 ? currentIndex : position;
                
            CurrentGun = null;
            for (int i = 0; i < localGuns.Count; i++)
            {
                if (currentIndex == i)
                {
                    localGuns[i].SetActive(true);
                    CurrentGun = localGuns[i].GetComponent<Shooting>();
                }
                else
                    localGuns[i].SetActive(false);
            }
        }
        
        public void AddGun(GameObject gun)
        {
            if (InventoryIsFull) return;

            GameObject instanceGun = Instantiate(gun, gunAnchor);
            instanceGun.SetActive(false);
            localGuns.Add(instanceGun);
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
            else if (Keyboard.current.digit3Key.wasPressedThisFrame)
            {
                Switch(2);
            }
        }
    }
}