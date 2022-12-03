using System;
using System.Collections.Generic;
using UnityEngine;

namespace Gun
{
    public class MuzzleFlash : MonoBehaviour
    {
        public List<Sprite> sprites;
        
        private int currentFrame = 0;
        private SpriteRenderer spriteRenderer;
        private Shooting shooting;
        private float timer;
        private float rpmMultiplier;
        private bool firstRun = true;

        private void OnEnable()
        {
            shooting ??= transform.parent.GetComponent<Shooting>();
            spriteRenderer ??= GetComponent<SpriteRenderer>();
            
            if (firstRun)
            {
                firstRun = false;
                gameObject.SetActive(false);
                return;
            }
            
            rpmMultiplier = Mathf.Clamp(shooting.FireTime / sprites.Count, 0, 0.05f);

            currentFrame = 0;
            timer += rpmMultiplier;
            spriteRenderer.sprite = sprites[currentFrame];
        }
        
        private void Update()
        {
            timer -= Time.deltaTime;

            if (!(timer <= 0)) return;
            
            timer += rpmMultiplier;
            currentFrame++;

            if (currentFrame >= sprites.Count)
            {
                gameObject.SetActive(false);
                return;
            }

            spriteRenderer.sprite = sprites[currentFrame];
        }
    }
}