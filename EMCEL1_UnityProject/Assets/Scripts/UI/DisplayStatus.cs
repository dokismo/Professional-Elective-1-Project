using System;
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

        private void Update()
        {
            txtMoney.text = $"MONEY: {playerStatusScriptable.money}";
            
            playerHealthBar.fillAmount = playerStatusScriptable.health / playerStatusScriptable.maxHealth;
            playerHealthBar.color = Color.Lerp(Color.red, Color.white, playerHealthBar.fillAmount * 3.5f);
        }
    }
}
