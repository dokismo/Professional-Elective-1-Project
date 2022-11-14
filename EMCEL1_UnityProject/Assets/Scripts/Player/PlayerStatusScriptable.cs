using System.Collections.Generic;
using UnityEngine;

namespace Player
{
    [CreateAssetMenu(menuName = "Player/Status", fileName = "PlayerStatus")]
    public class PlayerStatusScriptable : ScriptableObject
    {
        public int money;
        public float health;
        public float maxHealth;

        

        public int GetMoney(int amount)
        {
            if (money < amount) return -1;

            money -= amount;
            return amount;
        }

        public void AddMoney(int amount) => money += amount;

        public void AddHealthBy(float amount) => health = Mathf.Clamp(health + amount, 0, maxHealth);
    }
}