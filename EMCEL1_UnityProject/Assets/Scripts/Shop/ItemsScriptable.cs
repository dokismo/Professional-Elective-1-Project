using UnityEngine;

namespace Shop
{
    [System.Serializable]
    public class Item
    {
        public string name = "Not Named";
        public GameObject gameObject;
        public int value = 100;
    }
    
    [CreateAssetMenu(fileName = "ItemList", menuName = "Shop/Storage")]
    public class ItemsScriptable : ScriptableObject
    {
        [SerializeField] private Item[] items;

        public Item GetItem(int position)
        {
            if (position < items.Length) return items[position];
            
            Debug.LogError($"NO ITEM FOUND {position} {this}");
            return default;

        }
    }
}