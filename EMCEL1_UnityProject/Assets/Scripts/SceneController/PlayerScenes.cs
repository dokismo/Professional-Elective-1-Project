using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace SceneController
{
    public class PlayerScenes : MonoBehaviour
    {
        public List<string> scenesName;

        private void Start()
        {
            LoadScenes();
        }

        private void LoadScenes()
        {
            foreach (var sceneName in scenesName)
            {
                SceneManager.LoadScene(sceneName, LoadSceneMode.Additive);
            }
        }

        private void OnDestroy()
        {
            foreach (var sceneName in scenesName)
            {
                SceneManager.UnloadSceneAsync(sceneName);
            }
        }
    }
}