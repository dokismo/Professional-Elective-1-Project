using System;
using Player;
using UnityEngine;
using UnityEngine.UI;

namespace UI
{
    public class CameraMouseSensitivity : MonoBehaviour
    {
        public Slider slider;
        public PlayerStatusScriptable playerStatusScriptable;

        private const string MouseSensitivity = "MouseSensitivity";

        private void Awake()
        {
            slider.onValueChanged.AddListener(playerStatusScriptable.SetMouseSensitivity);
        }

        private void OnEnable()
        {
            slider.value = PlayerPrefs.GetFloat(MouseSensitivity, 10);
        }

        private void OnDisable()
        {
            PlayerPrefs.SetFloat(MouseSensitivity, slider.value);
        }
    }
}