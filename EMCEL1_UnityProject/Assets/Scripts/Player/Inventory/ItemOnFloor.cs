using CORE;
using UnityEngine;

namespace Player.Inventory
{
    public class ItemOnFloor : MonoBehaviour, IPickup<ItemType>
    {
        public ItemType itemType;
        public GameObject itemPrefab;
        
        public GameObject GetItem() => itemPrefab;
        
        public ItemType GetItemType()
        {
            return itemType;
        }
    }
}