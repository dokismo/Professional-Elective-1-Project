using UnityEngine;
using UnityEngine.UI;

namespace UI.In_Game_UI
{
    public class HealthBar : MonoBehaviour
    {
        public Image hpBar;
        
        public float playerHealth = 100f;
        public float playerMaxHealth = 100f;

        public Color healthyColor, dyingColor;

        private void Update()
        {
            float value = playerHealth / playerMaxHealth;
            
            hpBar.fillAmount = value;
            hpBar.color = Color.Lerp(dyingColor, healthyColor, value);
        }
    }
}