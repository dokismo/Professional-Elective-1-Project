using System;
using Player.Dialogue;
using Player.Status_Effects.AppliedStatus;
using UI.MainMenu;
using UnityEngine;

namespace SceneController
{
    public class InformationHolder : MonoBehaviour
    {
        public static InformationHolder instance; 
        
        public static Action removeIcon;
        public static Action removeDialogue;
        
        public static Action<string, DialogueScriptable> setAnimatorController;
        public static Action<float> setMouseSensitivity;
        
        public static Func<RuntimeAnimatorController> getAnimatorController;
        public static Func<float> getMouseSensitivity;

        public IconsAnimatorControllers iconsAnimatorControllers;
        public RuntimeAnimatorController runtimeAnimatorController;
        public DialogueScriptable dialogueScriptable;
        public BaseAppliedStatusScriptable baseAppliedStatusScriptable;
        private float mouseSensitivity;

        private void Awake()
        {
            if (InformationHolder.instance == null) instance = this;
        }

        private void Start()
        {
            mouseSensitivity = PlayerPrefs.GetFloat("MouseSensitivity", 10f);
        }

        private void OnEnable()
        {
            setAnimatorController += SetController;
            getAnimatorController += GetController;
            removeIcon += RemoveIcon;
            
            setMouseSensitivity += SetMouseSensitivity;
            getMouseSensitivity += GetSensitivity;
            removeDialogue += RemoveDialogue;
        }

        private void OnDisable()
        {
            setAnimatorController -= SetController;
            getAnimatorController -= GetController;
            removeIcon -= RemoveIcon;
            
            setMouseSensitivity -= SetMouseSensitivity;
            getMouseSensitivity -= GetSensitivity;
            removeDialogue -= RemoveDialogue;
        }

        private void RemoveDialogue()
        {
            dialogueScriptable = null;
        }

        private float GetSensitivity() => 
            mouseSensitivity;
        private void SetMouseSensitivity(float obj) => 
            mouseSensitivity = obj;

        private void RemoveIcon()
        {
            runtimeAnimatorController = null;
        }

        private RuntimeAnimatorController GetController()
        {
            return runtimeAnimatorController;
        }
        
        private void SetController(string controllerName, DialogueScriptable dlgScriptable)
        { 
            runtimeAnimatorController = iconsAnimatorControllers.GetController(controllerName);
            dialogueScriptable = dlgScriptable;
        } 
            
    }
}