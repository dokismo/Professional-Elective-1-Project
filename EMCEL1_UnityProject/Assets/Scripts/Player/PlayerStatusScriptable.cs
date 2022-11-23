using System.Collections.Generic;
using Player.Control;
using UnityEngine;

namespace Player
{
    [CreateAssetMenu(menuName = "Player/Status", fileName = "PlayerStatus")]
    public class PlayerStatusScriptable : ScriptableObject
    {
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

        public void SetHealthBy(int amount) => health = Mathf.Clamp(health + amount, 0, maxHealth);
        
        
    }
}