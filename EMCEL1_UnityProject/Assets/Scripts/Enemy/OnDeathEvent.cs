using System;
using UnityEngine;

namespace Enemy
{
    public class OnDeathEvent : MonoBehaviour
    {
        public delegate void OnDeath(int money);
        public static OnDeath givePlayerMoney;

       
        public int deathValue = 100;

        private void OnDestroy()
        {
            // FindObjectOfType<AudioManager>().Play("zombie_death01"); //josh_sfx
            givePlayerMoney?.Invoke(deathValue);
        }
    }
}
