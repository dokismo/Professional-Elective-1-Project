using System.Collections.Generic;
using System.Linq;
using Inventory.Items;
using Inventory.Items.Guns;
using UnityEngine;

namespace Inventory
{
    [CreateAssetMenu(fileName = "Inventory", menuName = "Inventory")]
    public class Inventory : ScriptableObject
    {
        public int maxItems = 3;
        [SerializeField]
        private List<LocalGun> container;
        
        public void AddGun(GameObject gameObject, ItemObject itemObject)
        {
            if (ItemType.Gun == itemObject.ItemScriptable.itemType)
                AddGun(gameObject, itemObject.ItemScriptable, itemObject.Amount);
            else if (ItemType.Ammo == itemObject.ItemScriptable.itemType)
                AddAmmo(gameObject, itemObject.ItemScriptable.GetAmmoScriptable());
        }
        
        public void AddGun(GameObject gameObject, ItemScriptable itemScriptable, int amount)
        {
            container ??= new List<LocalGun>();

            if (container.Count >= maxItems) return;
            
            Destroy(gameObject);
            
            container.Add(new LocalGun(itemScriptable.itemName, amount, itemScriptable.itemType, itemScriptable.GetAmmoType(), itemScriptable.gameObject));
        }

        public void Remove(string itemName)
        {
            foreach (var item in container.Where(item => item.Name == itemName))
            {
                Remove(item);
                return;
            }
        }
        public void Remove(LocalGun localGun)
        {
            container.Remove(localGun);
        }

        public void AddAmmo(GameObject gameObject, AmmoScriptable ammoScriptable)
        {
            foreach (var gun in container)
            {
                if (gun.ammoType != ammoScriptable.ammoType) continue;
                
                Destroy(gameObject);
                
                gun.ammo += ammoScriptable.amount;
                break;
            }
        }
    }

    [System.Serializable]
    public class LocalGun
    {
        public readonly string Name;
        public int ammo;
        public ItemType itemType;
        public AmmoType ammoType;
        public GameObject gameObject;

        public LocalGun(string name, int ammo, ItemType itemType, AmmoType ammoType, GameObject gameObject)
        {
            Name = name;
            this.ammo = ammo;
            this.itemType = itemType;
            this.ammoType = ammoType;
            this.gameObject = gameObject;
        }
    }
}