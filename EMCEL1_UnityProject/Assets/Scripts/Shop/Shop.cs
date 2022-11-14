using Player;
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
    
    
    public class Shop : MonoBehaviour
    {
        [SerializeField] private Item[] items;
        public PlayerStatusScriptable playerStatusScriptable;

        public GameObject Buy(int position)
        {
            return playerStatusScriptable.GetMoney(items[position].value) > -1 ? items[position].gameObject : null;
        }
    }
}