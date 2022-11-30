using System;
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
            IsLookingAtItem();
            Buy();
        }

        private void Buy()
        {
            if (!Keyboard.current.eKey.wasPressedThisFrame || currentWallShop == null) return;

            GameObject boughtGun = currentWallShop.BuyItem(playerStatusScriptable);

            if (boughtGun == null) 
                return; 
            
            playerStatusScriptable.PlayerStatus.AddGun(boughtGun);
            
            
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