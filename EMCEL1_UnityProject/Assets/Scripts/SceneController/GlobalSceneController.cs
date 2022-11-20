using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace SceneController
{
    [Serializable]
    public class SceneInstance
    {
        public string name;
        public bool isLoaded;

        public void LoadAsynchronous(LoadSceneMode mode)
        {
            if (isLoaded) return;
            
            SceneManager.LoadSceneAsync(name, mode);
        }
        
        public void Load(LoadSceneMode mode)
        {
            if (isLoaded) return;

            SceneManager.LoadScene(name, mode);
        }

        public void Unload()
        {
            if (!isLoaded) return;
            
            SceneManager.UnloadSceneAsync(name);
        }
    }

    [Serializable]
    public class SceneGroup
    {
        public string name;
        
        public List<SceneInstance> sceneList;

        public void LoadEverything(LoadSceneMode mode)
        {
            foreach (var scene in sceneList) scene.Load(mode);
        }

        public void LoadAsynchronousEverything(LoadSceneMode mode)
        {
            foreach (var scene in sceneList) scene.Load(mode);
        }

        public void UnloadEverything()
        {
            foreach (var scene in sceneList) scene.Unload();
        }
    }
    
    
    public class GlobalSceneController : MonoBehaviour
    {
        #region Singleton
        private static GlobalSceneController Instance { get; set; }

        private void Awake()
        {
            if (Instance != null && Instance != this)
                Destroy(this);
            else
            {
                Instance = this;
                DontDestroyOnLoad(this);
            }
        }
        #endregion

        public delegate void LoadSceneEvent(string name);

        public static LoadSceneEvent loadSingleEvent;
        public static LoadSceneEvent loadAdditiveEvent;
        public static LoadSceneEvent asyncLoadSingleEvent;
        public static LoadSceneEvent asyncLoadAdditiveEvent;
        
        [SerializeField] private ScenesListScriptable scenesListScriptable;
        
        private List<SceneGroup> SceneGroups => scenesListScriptable.SceneGroups;
        private List<SceneGroup> loadedScenes;

        public void OnEnable()
        {
            loadSingleEvent += LoadSingle;
            loadAdditiveEvent += LoadAdditive;
            asyncLoadSingleEvent += LoadAsyncSingle;
            asyncLoadAdditiveEvent += LoadAsyncAdditive;
        }

        private void OnDisable()
        {
            loadSingleEvent -= LoadSingle;
            loadAdditiveEvent -= LoadAdditive;
            asyncLoadSingleEvent -= LoadAsyncSingle;
            asyncLoadAdditiveEvent -= LoadAsyncAdditive;
        }

        private void Start()
        {
            loadedScenes = new List<SceneGroup>();
        }

        private SceneGroup FindSceneGroup(string groupName)
        {
            foreach (var sceneGroup in SceneGroups)
            {
                if (sceneGroup.name == groupName)
                    return sceneGroup;
            }

            Debug.LogError($"{groupName} NOT FOUND IN THE LIST {this}");
            return null;
        }

        private SceneGroup FindLoadedSceneGroup(string groupName)
        {
            foreach (var sceneGroup in loadedScenes)
            {
                if (sceneGroup.name == groupName)
                    return sceneGroup;
            }

            Debug.LogError($"{groupName} NOT FOUND IN THE LOADED LIST {this}");
            return null;
        }

        private void Load(SceneGroup sceneGroup, LoadSceneMode mode)
        {
            if (sceneGroup == null) return;
            
            UnloadEverything();
            loadedScenes.Add(sceneGroup);
            sceneGroup.LoadEverything(mode);
        }

        private void LoadAsynchronous(SceneGroup sceneGroup, LoadSceneMode mode)
        {
            if (sceneGroup == null) return;
            
            UnloadEverything();
            loadedScenes.Add(sceneGroup);
            sceneGroup.LoadAsynchronousEverything(mode);
        }

        private void LoadSingle(string groupName) => Load(FindSceneGroup(groupName), LoadSceneMode.Single);
        private void LoadAdditive(string groupName) => Load(FindSceneGroup(groupName), LoadSceneMode.Additive);
        private void LoadAsyncSingle(string groupName) => LoadAsynchronous(FindSceneGroup(groupName), LoadSceneMode.Single);
        private void LoadAsyncAdditive(string groupName) => LoadAsynchronous(FindSceneGroup(groupName), LoadSceneMode.Additive);
        
        private void Unload(string groupName)
        {
            var loadedScene = FindLoadedSceneGroup(groupName);

            if (loadedScene == null) return;
            
            loadedScenes.Remove(loadedScene);
            FindSceneGroup(groupName).UnloadEverything();
        }

        private void UnloadEverything()
        {
            if (loadedScenes.Count < 1) return;
            foreach (var loadedScene in loadedScenes) loadedScene.UnloadEverything();
            loadedScenes.Clear();
        }

        private void OnApplicationQuit()
        {
            UnloadEverything();
        }
    }
}
