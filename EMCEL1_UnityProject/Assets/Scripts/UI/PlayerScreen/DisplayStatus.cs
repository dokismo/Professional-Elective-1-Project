using Gun;
using Player;
using Player.Display;
using SceneController;
using Shop;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace UI.PlayerScreen
{
    public class DisplayStatus : MonoBehaviour
    {
        public delegate void Event();
        public static Event onDead;
        
        public PlayerStatusScriptable playerStatusScriptable;
        public Color staminaFullColor;

        public float blinkHighestRandomInterval = 10f;
        
        public Image playerHealthBar;
        public Animator iconAnimator;
        public TextMeshProUGUI txtMoney;
        public Image primaryIcon, secondaryIcon;
        public TextMeshProUGUI ammo;
        public TextMeshProUGUI shopTxt;
        public TextMeshProUGUI killCountTxt;
        public GameObject dead;
        public GameObject paused;
        public Image staminaBG, staminaBar;

        private float staminaVisibilityTimerA;
        private float staminaVisibilityTimerB;
        private float staminaTimer;
        private WallShop currentWallShop;
        private float randomInterval;
        private static readonly int Play = Animator.StringToHash("Play");

        private void OnEnable()
        {
            InspectShopWall.showWallShop += ItemShop;
            GlobalCommand.setPause += SetPause;
            PlayerStatusScriptable.staminaChanged += StaminaChangedEvent;
            
            paused.SetActive(false);
            onDead += OnDead;
        }

        private void OnDisable()
        {
            InspectShopWall.showWallShop -= ItemShop;
            onDead -= OnDead;
            GlobalCommand.setPause -= SetPause;
            PlayerStatusScriptable.staminaChanged -= StaminaChangedEvent;
        }

        private void Start()
        {
            randomInterval = Random.Range(0, blinkHighestRandomInterval);
            staminaBG.color = staminaBar.color = Color.clear; 
            shopTxt.enabled = false;

            var test = InformationHolder.instance.runtimeAnimatorController;
            iconAnimator.runtimeAnimatorController = test;

            Debug.Log(test.name);
        }
        
        private void StaminaChangedEvent()
        {
            staminaVisibilityTimerA = 0;
            staminaVisibilityTimerB = 1;
        }

        private void Update()
        {
            Money();
            Health();
            Ammo();
            GunIcon();
            SetStamina();
            Blink();
            SetKillCount();
        }

        private void SetKillCount()
        {
            killCountTxt.text = $"Kill Count: {playerStatusScriptable.killCount}";
        }

        private void Blink()
        {
            randomInterval -= Time.deltaTime;
            
            if (!(randomInterval <= 0)) return;
            
            randomInterval = Random.Range(0, blinkHighestRandomInterval);
            iconAnimator.SetTrigger(Play);
        }

        private void SetStamina()
        {
            if (staminaVisibilityTimerA < 1)
            {
                staminaVisibilityTimerA = Mathf.Clamp01(staminaVisibilityTimerA + Time.deltaTime);
                staminaTimer = Mathf.Clamp01(staminaTimer + Time.deltaTime);
            }
            else
            {
                staminaVisibilityTimerB = Mathf.Clamp01(staminaVisibilityTimerB - Time.deltaTime);
                staminaTimer = Mathf.Clamp01(staminaTimer - Time.deltaTime);;
            }
            
            staminaBar.fillAmount = playerStatusScriptable.stamina / playerStatusScriptable.maxStamina;
            staminaBG.color = staminaBar.color = Color.Lerp(Color.clear, staminaFullColor, staminaTimer);
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
