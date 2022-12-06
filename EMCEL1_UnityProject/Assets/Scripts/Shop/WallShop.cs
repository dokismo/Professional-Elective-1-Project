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
        public static Action itemBought;
        
        public ShopItem item;

        public String GetMessage() => 
            $"NAME: {item.name}\nPRICE: {item.price}\nRefill: {item.RefillPrice}";

        public GameObject InspectItem() => item.gun;

        public GameObject BuyItem(PlayerStatusScriptable pScript)
        {
            GameObject boughtGun = pScript.GetMoney(item.price) < 0 ? null : item.gun;

            if (boughtGun != null)
            {
                itemBought?.Invoke();
            }

            return boughtGun;
        }
            

        public bool BuyRefill(PlayerStatusScriptable pScript) => pScript.GetMoney(item.RefillPrice) >= 0;
    }
}