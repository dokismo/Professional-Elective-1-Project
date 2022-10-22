using System;
using System.Collections.Generic;
using UnityEngine;

namespace SceneManagement
{
    public class GlobalSceneManager : MonoBehaviour
    {
        public delegate void LoadMainSceneEvent<in TT>(TT name);
        public static LoadMainSceneEvent<string> LoadMainScene;
        public static LoadMainSceneEvent<string> LoadAdditiveScene;
        public static LoadMainSceneEvent<string> UnloadAdditiveScene;
        
        [SerializeField] private SceneScriptableObject sceneScriptableObject;

        [SerializeField] private bool loadMainOnStart = true;
        
        
        private void Start()
        {
            if (loadMainOnStart)
                sceneScriptableObject.GoToMainMenu();
        }

        private void OnEnable()
        {
            LoadMainScene += sceneScriptableObject.LoadMain;
            LoadAdditiveScene += sceneScriptableObject.LoadAdditive;
            UnloadAdditiveScene += sceneScriptableObject.UnloadAdditive;
        }

        private void OnDisable()
        {
            LoadMainScene -= sceneScriptableObject.LoadMain;
            LoadAdditiveScene -= sceneScriptableObject.LoadAdditive;
            UnloadAdditiveScene -= sceneScriptableObject.UnloadAdditive;
        }

        private void OnApplicationQuit()
        {
            sceneScriptableObject.CurrentMainScene = null;
            sceneScriptableObject.CurrentAdditiveScenes = new List<LocalGroupScene>();
            
            foreach (var localGroupScene in sceneScriptableObject.mainScenes) localGroupScene.UnloadAll();
            foreach (var localGroupScene in sceneScriptableObject.additiveScenes) localGroupScene.UnloadAll();
        }
        
        
    }
}