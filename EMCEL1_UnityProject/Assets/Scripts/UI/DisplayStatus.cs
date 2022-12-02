using Gun;
using Player;
using Player.Display;
using Shop;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace UI
{
    public class DisplayStatus : MonoBehaviour
    {
        public delegate void Event();
        public static Event onDead;
        
        public PlayerStatusScriptable playerStatusScriptable;

        public Image playerHealthBar;
        public Image playerIcon;
        public TextMeshProUGUI txtMoney;
        public Image gunIcon;
        public TextMeshProUGUI ammo;
        public TextMeshProUGUI shopTxt;
        public GameObject dead;

        private WallShop currentWallShop;

        private void OnEnable()
        {
            InspectShopWall.showWallShop += ItemShop;
            onDead += OnDead;
        }

        private void OnDisable()
        {
            InspectShopWall.showWallShop -= ItemShop;
            onDead -= OnDead;
        }

        private void Start()
        {
            shopTxt.enabled = false;
            playerIcon.sprite = playerStatusScriptable.CharacterIcon;
        }

        private void Update()
        {
            Money();
            Health();
            Ammo();
            GunIcon();
        }

        private void GunIcon()
        {
            gunIcon.sprite = playerStatusScriptable.GetCurrentGunIcon();
            gunIcon.color = gunIcon.sprite != null
                ? Color.white
                : Color.clear;
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

        private void OnDead()
        {
            dead.SetActive(true);
        }
    }
}
