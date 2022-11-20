using System;
using System.Collections.Generic;
using Gun;
using UnityEngine;
using UnityEngine.InputSystem;

namespace Player.Control
{
    public class PlayerStatus : MonoBehaviour
    {
        public delegate void ChangeHealthEvent(int amount);
        public static ChangeHealthEvent changeHealth;
        
        public PlayerStatusScriptable playerStatusScriptable;

        public int maxGuns = 3;
        public List<GameObject> localGuns;
        public Transform gunAnchor;

        private int currentIndex = 0;

        public bool inventoryIsFull => localGuns.Count >= maxGuns;

        public static bool canShoot => Cursor.lockState == CursorLockMode.Locked;

        private void OnEnable()
        {
            Enemy.OnDeathEvent.givePlayerMoney += playerStatusScriptable.AddMoney;
            changeHealth += playerStatusScriptable.SetHealthBy;
        }

        private void OnDisable()
        {
            Enemy.OnDeathEvent.givePlayerMoney -= playerStatusScriptable.AddMoney;
            changeHealth -= playerStatusScriptable.SetHealthBy;
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

            Switch();
        }
        
        

        private void Update()
        {
            Selection();
        }
        
        public void Switch(int position = -1)
        {
            currentIndex = position < 0 ? currentIndex : position;
                
            for (int i = 0; i < localGuns.Count; i++)
            {
                localGuns[i].SetActive(currentIndex == i);
            }
        }
        
        public void AddGun(GameObject gun)
        {
            if (inventoryIsFull) return;

            GameObject instanceGun = Instantiate(gun, gunAnchor);
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