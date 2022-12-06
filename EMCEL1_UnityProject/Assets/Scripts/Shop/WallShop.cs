using System;
using Player;
using UnityEngine;

namespace Shop
{
    [Serializable]
    public class ShopItem
    {
        public string name;
        public int price;
        public int RefillPrice => Mathf.RoundToInt(price * 0.4f);
            
        public GameObject gun;
    }
    
    public class WallShop : MonoBehaviour
    {
        public ShopItem item;

        public String GetMessage() => 
            $"NAME: {item.name}\nPRICE: {item.price}\nRefill: {item.RefillPrice}";

        public GameObject InspectItem() => item.gun;

        public GameObject BuyItem(PlayerStatusScriptable pScript) =>
            pScript.GetMoney(item.price) < 0 
                ? null 
                : item.gun;

        public bool BuyRefill(PlayerStatusScriptable pScript) => pScript.GetMoney(item.RefillPrice) >= 0;
    }
}