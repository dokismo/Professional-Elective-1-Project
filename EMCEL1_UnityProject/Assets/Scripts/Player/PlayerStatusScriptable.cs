using System.Collections.Generic;
using UnityEngine;

namespace Player
{
    [CreateAssetMenu(menuName = "Player/Status", fileName = "PlayerStatus")]
    public class PlayerStatusScriptable : ScriptableObject
    {
        public List<GameObject> guns;
        public int maxGuns = 3;
        
        public int money;
        public float health;
        public float maxHealth;

        private GameObject gunAnchor;

        public void SetAnchor(GameObject localGunAnchor) => gunAnchor = localGunAnchor;

        public void AddGuns(GameObject gun)
        {
            if (guns.Count >= maxGuns) return;
            
            guns.Add(gun);
        }

        public void RemoveGun(GameObject gun)
        {
            if (gun == null && !guns.Contains(gun)) return;

            guns.Remove(gun);
            Destroy(gun);
        }

        public int GetMoney(int amount)
        {
            if (money < amount) return -1;

            money -= amount;
            return amount;
        }
        
        public void Switch(int position)
        {
            for (int i = 0; i < guns.Count; i++)
            {
                guns[i].SetActive(position == i);
            }
        }

        public void AddMoney(int amount) => money += amount;

        public void AddHealthBy(float amount) => health = Mathf.Clamp(health + amount, 0, maxHealth);
    }
}