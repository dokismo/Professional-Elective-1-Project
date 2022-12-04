using Gun;
using Player;
using Player.Display;
using SceneController;
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
        public Image primaryIcon, secondaryIcon;
        public TextMeshProUGUI ammo;
        public TextMeshProUGUI shopTxt;
        public GameObject dead;
        public GameObject paused;

        private WallShop currentWallShop;

        private void OnEnable()
        {
            InspectShopWall.showWallShop += ItemShop;
            GlobalCommand.setPause += SetPause;
            
            paused.SetActive(false);
            onDead += OnDead;
        }

        private void OnDisable()
        {
            InspectShopWall.showWallShop -= ItemShop;
            onDead -= OnDead;
            GlobalCommand.setPause -= SetPause;
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
            primaryIcon.sprite = playerStatusScriptable.GetPrimaryIcon();
            secondaryIcon.sprite = playerStatusScriptable.GetSecondaryIcon();
            
            primaryIcon.color = primaryIcon.sprite != null
                ? Color.white
                : Color.clear;
            
            secondaryIcon.color = secondaryIcon.sprite != null
                ? Color.white
                : Color.clear;
        }

        private void Ammo()
        {
            Shooting gun = playerStatusScriptable.PlayerStatus != null
                ? playerStatusScriptable.PlayerStatus.PrimaryGun
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

        private void SetPause(bool value)
        {
            paused.SetActive(value);
        }
    }
}
