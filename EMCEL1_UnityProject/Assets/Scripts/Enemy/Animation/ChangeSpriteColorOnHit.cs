using System;
using UnityEngine;

namespace Enemy.Animation
{
    public class ChangeSpriteColorOnHit : MonoBehaviour
    {
        public float colorTime = 0.5f;
        public Color appliedColor;

        private float timer;
        public bool toggle;
        
        private SpriteRenderer spriteRenderer;

        private void Start()
        {
            spriteRenderer = GetComponent<SpriteRenderer>();
        }

        private void Update()
        {
            if (!toggle) return;
            
            timer = Mathf.Clamp(timer - Time.deltaTime, 0, 9);
            toggle = timer != 0;
            spriteRenderer.color = Color.Lerp(Color.white, appliedColor, timer);
        }

        public void ApplyEffect()
        {
            toggle = true;
            timer = colorTime;
        }
    }
}
