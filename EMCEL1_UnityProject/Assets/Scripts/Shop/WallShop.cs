using System;
using Player;
using UnityEngine;


namespace Shop
{
    public enum ItemType
    {
        Gun,
        MedKit,
        End
    }
    
    [Serializable]
    public class ShopItem
    {
        public string name;
        public int price;
        public int RefillPrice => Mathf.RoundToInt(price * 0.4f);
        public ItemType itemType = ItemType.Gun;
            
        public GameObject gun;
    }
    
    public class WallShop : MonoBehaviour
    {
        public static Action itemBought;
        
        public ShopItem item;

        public string GetMessage()
        {
            return item.itemType == ItemType.MedKit 
                ? $"NAME: {item.name}\nPRICE: {item.price}" 
                : item.itemType == ItemType.Gun 
                ? $"NAME: {item.name}\nPRICE: {item.price}\nRefill: {item.RefillPrice}"
                : $"ESCAPE!!\nPrice: {item.price}";
        }

        public GameObject InspectItem() => item.gun;

        public GameObject BuyItem(PlayerStatusScriptable pScript)
        {
            GameObject boughtGun = pScript.GetMoney(item.price) < 0 ? null : item.gun;

            if (boughtGun != null) itemBought?.Invoke();

            return boughtGun;
        }
        
        public bool BuyMedKit(PlayerStatusScriptable pScript) => pScript.GetMoney(item.price) > 1;

        public bool Escape(PlayerStatusScriptable pScript) => pScript.GetMoney(item.price) > 1;
        
        public bool BuyRefill(PlayerStatusScriptable pScript) => pScript.GetMoney(item.RefillPrice) >= 0;

    }
}