using System;
using Player;
using SceneController;
using UnityEngine;
using UnityEngine.UI;

namespace UI
{
    public class CameraMouseSensitivity : MonoBehaviour
    {
        public Slider slider;

        private const string MouseSensitivity = "MouseSensitivity";

        private void Awake()
        {
            slider.onValueChanged.AddListener(InformationInvoke);
        }

        private void Start()
        {
            slider.value = PlayerPrefs.GetFloat(MouseSensitivity, 10);
        }

        private void InformationInvoke(float t) => InformationHolder.setMouseSensitivity?.Invoke(t);

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