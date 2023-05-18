using System;
using Gun;
using Item.Gun;
using SceneController;
using Shop;
using UnityEngine;
using UnityEngine.InputSystem;

namespace Player.Display
{
    public class InspectShopWall : MonoBehaviour
    {
        public delegate void ShowItemWall(WallShop wallShop);
        public static ShowItemWall showWallShop;

        public delegate void OnEndBought();
        public static event OnEndBought onEndBought;

        public PlayerStatusScriptable playerStatusScriptable;
        public float distance = 5f;
        public LayerMask targetlayer;
        
        [SerializeField] Camera localCamera;
        private WallShop currentWallShop;

        private void Start()
        {
            //localCamera = Camera.main;
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

            switch (currentWallShop.item.itemType)
            {
                case ItemType.Gun:
                    if (currentGun != null && 
                        currentWallShop.item.name.ToUpper() == currentGun.gunName.ToUpper())
                    {
                        if (!currentWallShop.BuyRefill(playerStatusScriptable) || !currentGun.CanBuyAmmo) return;
                        playerStatusScriptable.PlayerStatus.CurrentGun.RefillAmmo();
                        BuySound.buyEvent?.Invoke();
                    }
                    else
                    {
                        GameObject gunItem = currentWallShop.BuyItem(playerStatusScriptable);
                        if (gunItem == null) return;
                        playerStatusScriptable.PlayerStatus.AddGun(gunItem);
                        BuySound.buyEvent?.Invoke();
                    }
                    break;
                case ItemType.MedKit:
                    if (playerStatusScriptable.PlayerStatus.MedKitInventoryIsFull || !currentWallShop.BuyMedKit(playerStatusScriptable)) return;
                    
                    playerStatusScriptable.PlayerStatus.AddMedKit();
                    BuySound.buyEvent?.Invoke();
                    break;
                case ItemType.End:
                    if (!currentWallShop.Escape(playerStatusScriptable)) return;
                    OpenEndDoor();
                    //GameEnd.endTheGame?.Invoke();
                    break;
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
            BuySound.lookEvent?.Invoke();
            currentWallShop = wallShop;
            showWallShop?.Invoke(currentWallShop);
        }

        public void OpenEndDoor()
        {
            GameObject.Find("End Door").GetComponent<Animator>().SetBool("Escape", true);
            Destroy(GameObject.Find("Open End Door"));
            NEWSpawningScript.Instance.IsEndDoorOpen = true;
            StartCoroutine(CameraEffectsHandler.Instance.CameraShake(10f, 0.1f));
            GameObject.Find("Door Sound").GetComponent<AudioSource>().Play();
            onEndBought?.Invoke();
        }
    }
}