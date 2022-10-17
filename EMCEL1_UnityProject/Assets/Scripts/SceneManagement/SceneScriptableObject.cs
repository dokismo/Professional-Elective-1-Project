using System;
using System.Collections.Generic;
using CORE;
using UnityEngine;

using UnityEngine.SceneManagement;
using UnityEngine.Serialization;

namespace SceneManagement
{
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
        
        [FormerlySerializedAs("sceneGroup")] 
        public List<LocalGroupScene> mainSceneGroup;
        public List<LocalGroupScene> mapLocations;

        public List<LocalGroupScene> AdditiveScenes { get; set; } = new();
        public LocalGroupScene CurrentMainScene { get; set; }
        
        private void LoadSingleLocalGroupScene(LocalGroupScene localGroupScene)
        {
            if (localGroupScene.IsLoaded) return;

            CurrentMainScene?.UnloadAll();
            CurrentMainScene = localGroupScene;
            CurrentMainScene.LoadAsync();
        }

        private void LoadAdditiveLocalGroupScene(LocalGroupScene localGroupScene)
        {
            if (localGroupScene.IsLoaded) return;
            
            AdditiveScenes.Add(localGroupScene);
            localGroupScene.LoadAsync();
        }

        private LocalGroupScene FindMainGroupScene(string sceneGroupName) =>
            FindGroupScene(mainSceneGroup, sceneGroupName);

        private LocalGroupScene FindLocationGroupScene(string sceneGroupName) =>
            FindGroupScene(mapLocations, sceneGroupName);

        private LocalGroupScene FindGroupScene(List<LocalGroupScene> localGroupScenes, string sceneGroupName)
        {
            foreach (var localGroupScene in localGroupScenes)
                if (localGroupScene.name == sceneGroupName)
                    return localGroupScene;

            Debug.LogError($"NO SCENE GROUP FOUND {this}");
            return null;
        }

        public void LoadLocalGroupScene(string groupSceneName)
        {
            var localGroupScene = FindMainGroupScene(groupSceneName);
            LoadSingleLocalGroupScene(localGroupScene);
        }

        public void GoToMainMenu() => LoadLocalGroupScene("Main Menu");

    }
}