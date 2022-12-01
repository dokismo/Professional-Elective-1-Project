using System;
using Player.Control;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;


namespace UI
{
    public class BloodEffect : MonoBehaviour
    {
        public float time = 1;
        
        private Image blood;
        private float timer;

        private void OnEnable()
        {
            PlayerStatus.changeHealth += ResetEffect;
        }

        private void OnDisable()
        {
            PlayerStatus.changeHealth -= ResetEffect;
        }

        private void Start()
        {
            blood = GetComponent<Image>();
        }

        private void ResetEffect(int amount)
        {
            timer = time;
        }

        private void Update()
        {
            timer = Mathf.Clamp01(timer - Time.deltaTime);
            blood.color = Color.Lerp(Color.clear, Color.white, timer);
        }
    }
}