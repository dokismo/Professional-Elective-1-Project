using System;
using System.Collections.Generic;
using UnityEngine;

using UnityEngine.SceneManagement;

namespace SceneManagement
{
    [Serializable]
    public class LocalScene
    {
        public string name;
        public bool isLoaded;
    }
    
    
    [Serializable] 
    public class LocalGroupScene
    {
        public string name;
        public List<LocalScene> scenes;

        public bool IsLoaded { get; private set; }
        
        public void LoadAsync()
        {
            IsLoaded = true;
            
            foreach (var scene in scenes)
            {
                if (scene.isLoaded) continue;
                
                SceneManager.LoadSceneAsync(scene.name, LoadSceneMode.Additive);
                scene.isLoaded = true;
            }
        }

        public void UnloadAll()
        {
            IsLoaded = false;
            foreach (var scene in scenes)
            {
                if (!scene.isLoaded) continue;
                
                SceneManager.UnloadSceneAsync(scene.name);
                scene.isLoaded = false;
            }
        }
    }
    
    [CreateAssetMenu(fileName = "Scene Management Scriptable", menuName = "Scene Management")]
    public class SceneScriptableObject : ScriptableObject
    {
        public delegate void LoadSceneEvent<in TT>(TT name);
        public static LoadSceneEvent<string> LoadScene;
        
        public List<LocalGroupScene> sceneGroup;
        public LocalGroupScene CurrentMainScene { get; set; }
        
        private void LoadLocalGroupScene(LocalGroupScene localGroupScene)
        {
            if (localGroupScene.IsLoaded) return;

            CurrentMainScene?.UnloadAll();
            CurrentMainScene = localGroupScene;
            CurrentMainScene.LoadAsync();
        }

        private void UnloadCurrentScene(LocalGroupScene localGroupScene)
        {
            localGroupScene.UnloadAll();
        }

        private LocalGroupScene FindGroupScene(string sceneGroupName)
        {
            foreach (var localGroupScene in sceneGroup)
                if (localGroupScene.name == sceneGroupName)
                    return localGroupScene;

            Debug.LogError($"NO SCENE GROUP FOUND {this}");
            return null;
        }

        public void LoadLocalGroupScene(string groupSceneName)
        {
            var localGroupScene = FindGroupScene(groupSceneName);
            LoadLocalGroupScene(localGroupScene);
        }

        public void GoToMainMenu() => LoadLocalGroupScene("Main Menu");

    }
}