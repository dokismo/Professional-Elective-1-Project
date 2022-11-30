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
        
        public GameObject gun;
    }
    
    public class WallShop : MonoBehaviour
    {
        public ShopItem item;

        public String GetMessage() => 
            $"NAME: {item.name}\nPRICE: {item.price}";

        public GameObject BuyItem(PlayerStatusScriptable pScript) =>
            pScript.GetMoney(item.price) < 0 
                ? null 
                : item.gun;
    }
}