using System;
using UI.MainMenu;
using UnityEngine;

namespace SceneController
{
    public class InformationHolder : MonoBehaviour
    {
        public static Action removeIcon;
        public static Action<string> setAnimatorController;
        public static Action<float> setMouseSensitivity;
        public static Func<RuntimeAnimatorController> getAnimatorController;
        public static Func<float> getMouseSensitivity;

        public IconsAnimatorControllers iconsAnimatorControllers;
        
        private RuntimeAnimatorController runtimeAnimatorController;
        private float mouseSensitivity;

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
            getMouseSensitivity = GetMouseSensitivity;
            
        }

        private void OnDisable()
        {
            setAnimatorController -= SetController;
            getAnimatorController -= GetController;
            removeIcon -= RemoveIcon;
            
            setMouseSensitivity -= SetMouseSensitivity;
            getMouseSensitivity -= GetMouseSensitivity;
            
        }

        private float GetMouseSensitivity() => mouseSensitivity;

        private void SetMouseSensitivity(float obj) => mouseSensitivity = obj;

        private void RemoveIcon() => runtimeAnimatorController = null;

        private RuntimeAnimatorController GetController() => runtimeAnimatorController;

        private void SetController(string controllerName) => 
            runtimeAnimatorController = iconsAnimatorControllers.GetController(controllerName);
    }
}