using System;
using Player;
using TMPro;
using UnityEngine;

namespace UI
{
    public class DisplayStatus : MonoBehaviour
    {
        public PlayerStatusScriptable playerStatusScriptable;

        public TextMeshProUGUI txtMoney;

        private void Update()
        {
            txtMoney.text = $"MONEY: {playerStatusScriptable.money}";   
        }
    }
}
