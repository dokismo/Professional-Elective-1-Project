using System;
using System.Collections.Generic;
using CORE;
using UnityEngine;

namespace Player.Inventory
{
    public enum ItemType
    {
        Gun,
        Health,
        Ammo
    }
    
    public enum AmmoType
    {
        Rifle,
        Pistol,
        Shotgun
    }
    
    public class Inventory : MonoBehaviour
    {
        public int maxGuns = 3;
        public List<GameObject> weapons;

        public GameObject gunAnchor;

        public int currentInvIndex = 0;

        public bool CanPickUpWeapon => weapons.Count < maxGuns;

        private void Start()
        {
            Switch(currentInvIndex);
        }

        private void Update()
        {
            CheckSwitchGun();
        }

        private void CheckSwitchGun()
        {
            if (Input.GetKeyDown(KeyCode.Alpha1))
            {
                Switch(0);
            }
            else if (Input.GetKeyDown(KeyCode.Alpha2))
            {
                Switch(1);
            }
            else if (Input.GetKeyDown(KeyCode.Alpha3))
            {
                Switch(2);
            }
        }

        private void Switch(int i)
        {
            currentInvIndex = i;
            
            for (int x = 0; x < weapons.Count; x++)
            {
                weapons[x].SetActive(x == i);
            }
        }

        private void AddGun(GameObject gun)
        {
            if (weapons.Count > maxGuns) return;

            GameObject localGun = Instantiate(gun, gunAnchor.transform);
            gun.transform.localPosition = new Vector3();
            localGun.SetActive(false);
            
            Switch(currentInvIndex);
            
            weapons.Add(localGun);
        }

        private void AddAmmo(AmmoType ammoType, int amount, GameObject ammoGameObject = null)
        {
            foreach (var weaponObject in weapons)
            {
                var weapon = weaponObject.GetComponent<Shooting>();

                if (weapon.ammoType != ammoType) continue;
                
                weapon.AddAmmo(amount);
                    
                if (ammoGameObject) Destroy(ammoGameObject);
                return;
            }
        }

        private void AddAmmo(GameObject ammoObj, IPickup<ItemType> pickup)
        {
            Ammo ammo = pickup.GetItem().GetComponent<Ammo>();
            
            AddAmmo(ammo.ammoType, ammo.amount, ammoObj);
        }
        

        private void OnTriggerEnter2D(Collider2D col)
        {
            IPickup<ItemType> pickup = col.gameObject.GetComponent<IPickup<ItemType>>();
            
            Debug.Log("test");

            if (pickup == null) return;

            ItemType itemType = pickup.GetItemType();

            if (itemType == ItemType.Gun && CanPickUpWeapon)
            {
                AddGun(pickup.GetItem());
                Destroy(col.gameObject);
            }
            else if (itemType == ItemType.Ammo)
            {
                AddAmmo(col.gameObject, pickup);
            }
        }
    }
}