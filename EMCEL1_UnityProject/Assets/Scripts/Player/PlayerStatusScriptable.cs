using System;
using Player.Control;
using UI;
using UI.MainMenu;
using UI.PlayerScreen;
using UnityEngine;

namespace Player
{
    [CreateAssetMenu(menuName = "Player/Status", fileName = "PlayerStatus")]
    public class PlayerStatusScriptable : ScriptableObject
    {
        public static Action staminaChanged;
        public static Action playerLowHealth;
        public static Action playerTakeDamage;
        
        public int money;
        public float health;
        public float maxHealth;
        public float stamina;
        public float maxStamina = 4;
        public int killCount;

        public PlayerStatus PlayerStatus { get; private set; }
        public bool CanSprint => stamina > 0;

        private bool lowHealthEventTrigger = true;

        public void SetPlayer(PlayerStatus playerStatus) => PlayerStatus = playerStatus;

        public int GetMoney(int amount)
        {
            if (money < amount) return -1;

            money -= amount;
            return amount;
        }

        public void PutMoney(int amount) => money += amount;

        public void AddMoney(int amount) => money += amount;

        public void SetHealthBy(int amount)
        {
            health = Mathf.Clamp(health + amount, 0, maxHealth);

            if (amount < 0)
            {
                if (health <= 0) DisplayStatus.onDead?.Invoke();
                else playerTakeDamage?.Invoke();
            }
            
            
            switch (amount)
            {
                case < 0 when health <= 25 && lowHealthEventTrigger:
                    lowHealthEventTrigger = false;
                    playerLowHealth?.Invoke();
                    break;
                case > 0 when health > 50 && !lowHealthEventTrigger:
                    lowHealthEventTrigger = true;
                    break;
            }

            
        }

        public void SetStaminaBy(float value)
        {
            if (value < 0)
                staminaChanged?.Invoke();

            stamina = Mathf.Clamp(stamina + value, 0, maxStamina);
        }

        public Sprite GetPrimaryIcon() => PlayerStatus != null && PlayerStatus.PrimaryGun != null && PlayerStatus.PrimaryGun.icon != null
            ? PlayerStatus.PrimaryGun.icon
            : null;

        public Sprite GetSecondaryIcon() => PlayerStatus != null && PlayerStatus.SecondaryGun != null && PlayerStatus.SecondaryGun.icon != null
            ? PlayerStatus.SecondaryGun.icon
            : null;
    }
}