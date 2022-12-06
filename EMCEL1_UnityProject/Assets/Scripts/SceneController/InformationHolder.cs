using System;
using UI.MainMenu;
using UnityEngine;

namespace SceneController
{
    public class InformationHolder : MonoBehaviour
    {
        public static InformationHolder instance; 
        
        public static Action removeIcon;
        
        public static Action<string> setAnimatorController;
        public static Action<float> setMouseSensitivity;
        
        public static Func<RuntimeAnimatorController> getAnimatorController;
        public static Func<float> getMouseSensitivity;

        public IconsAnimatorControllers iconsAnimatorControllers;
        public RuntimeAnimatorController runtimeAnimatorController;
        private float mouseSensitivity;

        private void Awake()
        {
            if (instance == null) instance = this;
        }

        private void Start()
        {
            mouseSensitivity = PlayerPrefs.GetFloat("MouseSensitivity", 10f);
        }

        private void OnEnable()
        {
            setAnimatorController = SetController;
            getAnimatorController = GetController;
            removeIcon -= RemoveIcon;
            
            setMouseSensitivity = SetMouseSensitivity;
            getMouseSensitivity = GetSensitivity;
        }

        private void OnDisable()
        {
            setAnimatorController -= SetController;
            getAnimatorController -= GetController;
            removeIcon -= RemoveIcon;
            
            setMouseSensitivity -= SetMouseSensitivity;
            getMouseSensitivity -= GetSensitivity;
        }

        private float GetSensitivity() => 
            mouseSensitivity;
        private void SetMouseSensitivity(float obj) => 
            mouseSensitivity = obj;

        private void RemoveIcon()
        {
            Debug.Log($"Remove Icon");
            runtimeAnimatorController = null;
        }

        private RuntimeAnimatorController GetController()
        {
            return runtimeAnimatorController;
        }
        
        private void SetController(string controllerName) => 
            runtimeAnimatorController = iconsAnimatorControllers.GetController(controllerName);
    }
}