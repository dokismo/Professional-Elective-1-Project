using CORE;
using UnityEngine;

namespace Inventory.Items
{
    public class ItemObject : MonoBehaviour, IPickup
    {
        [SerializeField]
        private ItemScriptable itemScriptable;
        [SerializeField]
        private int amount;
        
        public ItemScriptable ItemScriptable => itemScriptable;
        public int Amount => amount;
        
        public void Pickup()
        {
            
        }
    }
}