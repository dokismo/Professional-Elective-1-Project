using System;
using System.Collections.Generic;
using UnityEngine;

namespace Gun
{
    public class MuzzleFlash : MonoBehaviour
    {
        public List<Sprite> sprites;

        public bool uzi;
        
        private int currentFrame;
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

            currentFrame = uzi 
                ? currentFrame + 1
                : 0;
            
            timer = uzi 
                ? 0.02f
                : timer + rpmMultiplier;

            if (currentFrame >= sprites.Count) 
                currentFrame = 0;
            
            spriteRenderer.sprite = sprites[currentFrame];
        }
        
        private void Update()
        {
            timer -= Time.deltaTime;
            
            NonUziBetas();
            Uzi();
        }

        private void Uzi()
        {
            if (!uzi || !(timer <= 0)) return;
            
            gameObject.SetActive(false);
        }

        private void NonUziBetas()
        {
            if (uzi || !(timer <= 0)) return;

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