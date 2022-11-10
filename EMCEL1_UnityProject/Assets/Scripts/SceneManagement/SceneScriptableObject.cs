using System;
using System.Collections.Generic;
using System.Linq;
using CORE;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Serialization;

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
        [FormerlySerializedAs("sceneGroup")] 
        public List<LocalGroupScene> mainScenes;
        public List<LocalGroupScene> additiveScenes;

        public List<LocalGroupScene> CurrentAdditiveScenes { get; set; } = new();
        public LocalGroupScene CurrentMainScene { get; set; }
        
        public void LoadMain(string localGroupSceneName)
        {
            var localGroupScene = FindMainScene(localGroupSceneName);
            
            if (localGroupScene == null)
            {
                Debug.LogError($"CAN NOT FIND MAIN SCENE {this}");
                return;
            }
            
            LoadMain(localGroupScene);
        }
        
        public void LoadMain(LocalGroupScene localGroupScene)
        {
            if (localGroupScene.IsLoaded) return;

            CurrentMainScene?.UnloadAll();
            CurrentMainScene = localGroupScene;
            UnloadAllAdditive();
            CurrentMainScene.LoadAsync();
        }

        public void LoadAdditive(string localGroupSceneName)
        {
            var localGroupScene = FindAdditiveScene(localGroupSceneName);

            if (localGroupScene == null)
            {
                Debug.LogError($"CAN NOT FIND ADDITIVE SCENE {this}");
                return;
            }
            
            LoadAdditive(localGroupScene);
        }

        public void LoadAdditive(LocalGroupScene localGroupScene)
        {
            if (localGroupScene.IsLoaded) return;
            
            CurrentAdditiveScenes.Add(localGroupScene);
            localGroupScene.LoadAsync();
        }
    
        public void UnloadAdditive(string localGroupSceneName)
        {
            foreach (var localGroupScene in CurrentAdditiveScenes.Where(localGroupScene => localGroupScene.IsLoaded && localGroupScene.name == localGroupSceneName))
            {
                localGroupScene.UnloadAll();
                CurrentAdditiveScenes.Remove(localGroupScene);
                break;
            }
        }

        public void UnloadAllAdditive()
        {
            foreach (var localGroupScene in CurrentAdditiveScenes) localGroupScene.UnloadAll();
            CurrentAdditiveScenes = new List<LocalGroupScene>();
        }

        private LocalGroupScene FindMainScene(string sceneGroupName) =>
            FindGroupScene(mainScenes, sceneGroupName);

        private LocalGroupScene FindAdditiveScene(string sceneGroupName) =>
            FindGroupScene(additiveScenes, sceneGroupName);

        private LocalGroupScene FindGroupScene(List<LocalGroupScene> localGroupScenes, string sceneGroupName)
        {
            foreach (var localGroupScene in localGroupScenes)
                if (localGroupScene.name == sceneGroupName)
                    return localGroupScene;

            Debug.LogError($"NO SCENE GROUP FOUND {this}");
            return null;
        }

        public void GoToMainMenu() => LoadMain("Main Menu");
        
    }
}