using System;
using Player;
using Player.Control;
using UnityEngine;

namespace Shop
{
    public class Shop : MonoBehaviour
    {
        public GameObject shopUI;
        
        public ItemsScriptable itemsScriptable;
        public PlayerStatusScriptable playerStatusScriptable;
        
        private PlayerStatus PlayerStatus => playerStatusScriptable.PlayerStatus;

        private void OnEnable() => ShowShop.showState += SetState;
        private void OnDisable() => ShowShop.showState -= SetState;
        private void SetState(bool state) => shopUI.SetActive(state);
        
        public void Buy(int position)
        {
            Item item = itemsScriptable.GetItem(position);
            
            if (item.gameObject == null || PlayerStatus.InventoryIsFull)
                return;
        
            if (playerStatusScriptable.GetMoney(item.value) > -1 ? item.gameObject : null) 
                PlayerStatus.AddGun(item.gameObject);
        }

        public void RefillAmmo(int cost)
        {
            if (playerStatusScriptable.GetMoney(cost) <= 0) return;
            
            if (PlayerStatus.CurrentGun != null)
                PlayerStatus.CurrentGun.RefillAmmo();
        }
    }
}