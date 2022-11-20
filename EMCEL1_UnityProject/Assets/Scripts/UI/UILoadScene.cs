using System.Collections.Generic;
using SceneController;
using UnityEngine;

namespace UI
{
    public class UILoadScene : MonoBehaviour
    {
        public List<string> scenesToLoad;

        public void LoadSingle()
        {
            if (scenesToLoad.Count == 0) return;
            
            GlobalSceneController.loadSingleEvent?.Invoke(scenesToLoad[0]);
        }
        
        public void LoadAdditive()
        {
            foreach (var sceneName in scenesToLoad) GlobalSceneController.loadAdditiveEvent?.Invoke(sceneName);
        }

        public void AsyncLoadSingle()
        {
            if (scenesToLoad.Count == 0) return;
            
            GlobalSceneController.asyncLoadSingleEvent?.Invoke(scenesToLoad[0]);
        }

        public void AsyncLoadAdditive()
        {
            foreach (var sceneName in scenesToLoad) GlobalSceneController.asyncLoadAdditiveEvent?.Invoke(sceneName);
        }
    }
}