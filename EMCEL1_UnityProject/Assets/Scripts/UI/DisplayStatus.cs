using System;
using Gun;
using Player;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace UI
{
    public class DisplayStatus : MonoBehaviour
    {
        public PlayerStatusScriptable playerStatusScriptable;

        public Image playerHealthBar;
        public TextMeshProUGUI txtMoney;
        public TextMeshProUGUI ammo;

        private void Update()
        {
            Money();
            Health();
            Ammo();
        }

        private void Ammo()
        {
            Shooting gun = playerStatusScriptable.PlayerStatus.CurrentGun;
            ammo.text = gun
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
    }
}
