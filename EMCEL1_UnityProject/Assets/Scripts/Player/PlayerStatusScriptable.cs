using System;
using Player.Control;
using UI;
using UI.MainMenu;
using UI.PlayerScreen;
using UnityEngine;
using SceneController;
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
        public CharacterStatsMultiplier CharacterStatsMultiplier;
        public bool CanSprint => stamina > 0;

        private bool lowHealthEventTrigger = true;

        public void SetPlayer(PlayerStatus playerStatus) => PlayerStatus = playerStatus;

        public int GetMoney(int amount)
        {
            if (money < amount) return -1;

            money -= amount;
            return amount;
        }

        public void PutMoney(int amount) => money += (int)(amount * CharacterStatsMultiplier.MoneyMultiplier);

        public void AddMoney(int amount) => money += amount;

        public void SetHealthBy(int amount)
        {
            if(amount < 0)
            {
                amount = (int)(amount * CharacterStatsMultiplier.DamageReductionMultiplier);
            }
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

            if (value > 0) value = value * CharacterStatsMultiplier.StaminaRegenMultiplier;

            stamina = Mathf.Clamp(stamina + value, 0, maxStamina);
        }

        public Sprite GetPrimaryIcon()
        {
            if(PlayerStatus != null && PlayerStatus.PrimaryGun != null && PlayerStatus.PrimaryGun.icon != null)
            {
                return PlayerStatus.PrimaryGun.icon;
            } else if (PlayerStatus.PrimaryMelee != null)
            {
                return PlayerStatus.PrimaryMelee.icon;
            } else
            {
                return null;
            }
        }

        public Sprite GetSecondaryIcon()
        {
            if(PlayerStatus != null && PlayerStatus.SecondaryGun != null && PlayerStatus.SecondaryGun.icon != null)
            {
                return PlayerStatus.SecondaryGun.icon;
            }else if (PlayerStatus.SecondaryMelee != null)
            {
                return PlayerStatus.SecondaryMelee.icon;
            } else
            {
                return null;
            }
        }
    }
}