using System;
using UnityEngine;
using UnityEngine.UI;

namespace UI
{
    public class GameOverAnimation : MonoBehaviour
    {
        public Image bg, retry, exit;
        public float time;

        private float timer = 3;

        private void OnEnable()
        {
            timer = 0;
        }

        private void OnDisable()
        {
            bg.color = retry.color = exit.color = Color.clear;
        }

        private void Start()
        {
            bg.color = retry.color = exit.color = Color.clear;
        }

        private void Update()
        {
            timer = Mathf.Clamp(timer + Time.deltaTime, 0, time);

            bg.color = retry.color = exit.color =
                Color.Lerp(
                    Color.clear,
                    Color.white,
                    Mathf.InverseLerp(0, time, timer));
        }
    }
}
