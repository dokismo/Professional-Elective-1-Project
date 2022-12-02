using Player.Control;
using UI;
using UnityEngine;

namespace Player
{
    [CreateAssetMenu(menuName = "Player/Status", fileName = "PlayerStatus")]
    public class PlayerStatusScriptable : ScriptableObject
    {
        public Sprite CharacterIcon { get; private set; }

        public int money;
        public float health;
        public float maxHealth;

        public PlayerStatus PlayerStatus { get; private set; }

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

            if (health <= 0) DisplayStatus.onDead?.Invoke();
        }

        public void SetSprite(Sprite sprite) => CharacterIcon = sprite;

        public void RemoveSprite() => CharacterIcon = null;

        public Sprite GetCurrentGunIcon() => PlayerStatus.CurrentGun != null && PlayerStatus.CurrentGun.icon != null
            ? PlayerStatus.CurrentGun.icon
            : null;

    }
}