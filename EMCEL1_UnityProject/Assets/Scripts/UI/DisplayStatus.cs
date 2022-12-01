using System;
using Gun;
using Player;
using Player.Display;
using Shop;
using TMPro;
using UnityEngine;
using UnityEngine.Serialization;
using UnityEngine.UI;

namespace UI
{
    public class DisplayStatus : MonoBehaviour
    {
        public PlayerStatusScriptable playerStatusScriptable;

        public Image playerHealthBar;
        public TextMeshProUGUI txtMoney;
        public TextMeshProUGUI ammo;
        public TextMeshProUGUI shopTxt;

        private WallShop currentWallShop;

        private void OnEnable()
        {
            InspectShopWall.showWallShop += ItemShop;
        }

        private void OnDisable()
        {
            InspectShopWall.showWallShop -= ItemShop;
        }

        private void Start()
        {
            shopTxt.enabled = false;
        }

        private void Update()
        {
            Money();
            Health();
            Ammo();
        }

        private void Ammo()
        {

            Shooting gun = playerStatusScriptable.PlayerStatus != null
                ? playerStatusScriptable.PlayerStatus.CurrentGun
                : null;
            
            ammo.text = gun != null
                ? $"{gun.ammoInMag}/{gun.totalAmmo}"
                : "";
        } 

        private void Money()
        {
            txtMoney.text = $"MONEY: {playerStatusScriptable.money}";
        }

        private void Health()
        {
            playerHealthBar.fillAmount = playerStatusScriptable.health / playerStatusScriptable.maxHealth;
            playerHealthBar.color = Color.Lerp(Color.red, Color.white, playerHealthBar.fillAmount * 3.5f);
        }

        private void ItemShop(WallShop wallShop)
        {
            if (wallShop == null)
            {
                shopTxt.enabled = false;
                return;
            }
            
            shopTxt.SetText(wallShop.GetMessage() + "\n[E]");
            shopTxt.enabled = true;
        }
    }
}
