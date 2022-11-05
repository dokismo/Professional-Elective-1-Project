using Inventory.Items.Guns;
using UnityEngine;

namespace Inventory.Items
{
    public class ItemScriptable : ScriptableObject
    {
        public string itemName;
        public ItemType itemType;
        public GameObject gameObject;

        public virtual AmmoType GetAmmoType()
        {
            Debug.LogError($"THIS METHOD IS NOT SETUP {this}");
            return default;
        }

        public virtual AmmoScriptable GetAmmoScriptable()
        {
            Debug.LogError($"THIS METHOD IS NOT SETUP {this}");
            return default;
        }
    }

    public enum ItemType
    {
        Health,
        Gun,
        Ammo
    }

    public enum AmmoType
    {
        Pistol,
        Rifle,
        Shotgun
    }
}