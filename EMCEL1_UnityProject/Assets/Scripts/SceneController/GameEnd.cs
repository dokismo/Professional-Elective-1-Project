using System;
using UnityEngine;

namespace SceneController
{
    public class GameEnd : MonoBehaviour
    {
        public static Action endTheGame;

        public string endSceneName;

        private void OnEnable()
        {
            endTheGame += EndGame;
        }

        private void OnDisable()
        {
            endTheGame -= EndGame;
        }

        private void EndGame()
        {
            GlobalSceneController.loadSingleEvent?.Invoke(endSceneName);
        }
    }
}