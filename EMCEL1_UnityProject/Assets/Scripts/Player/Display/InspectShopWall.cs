using System;
using Gun;
using Shop;
using UnityEngine;
using UnityEngine.InputSystem;

namespace Player.Display
{
    public class InspectShopWall : MonoBehaviour
    {
        public delegate void ShowItemWall(WallShop wallShop);
        public static ShowItemWall showWallShop;

        public PlayerStatusScriptable playerStatusScriptable;
        public float distance = 5f;
        public LayerMask targetlayer;
        
        private Camera localCamera;
        private WallShop currentWallShop;

        private void Start()
        {
            localCamera = Camera.main;
        }

        private void Update()
        {
            if (!(Time.timeScale > 0.2f)) return;
            
            IsLookingAtItem();
            Buy();
        }

        private void Buy()
        {
            if (!Keyboard.current.eKey.wasPressedThisFrame || currentWallShop == null) return;

            Shooting currentGun = playerStatusScriptable.PlayerStatus.CurrentGun;

            if (playerStatusScriptable.PlayerStatus.CurrentGun != null && 
                currentWallShop.item.name.ToUpper() == currentGun.gunName.ToUpper())
            {
                if (!currentWallShop.BuyRefill(playerStatusScriptable) || !currentGun.CanBuyAmmo) return;
                
                playerStatusScriptable.PlayerStatus.CurrentGun.RefillAmmo();
            }
            else
            {
                GameObject boughtItem = currentWallShop.BuyItem(playerStatusScriptable);

                if (boughtItem == null) return;
                
                playerStatusScriptable.PlayerStatus.AddGun(boughtItem);
            }
        }

        private void IsLookingAtItem()
        {
            Ray ray = localCamera.ScreenPointToRay(Mouse.current.position.ReadValue());

            if (!Physics.Raycast(ray, out var raycastHit, distance, targetlayer))
            {
                if (currentWallShop != null)
                {
                    currentWallShop = null;
                    showWallShop?.Invoke(null);
                }
                
                return;
            }
            
            var wallShop = raycastHit.collider.gameObject.GetComponent<WallShop>();
           
            if (currentWallShop == wallShop)
                return;

            currentWallShop = wallShop;
            showWallShop?.Invoke(currentWallShop);
        }
    }
}