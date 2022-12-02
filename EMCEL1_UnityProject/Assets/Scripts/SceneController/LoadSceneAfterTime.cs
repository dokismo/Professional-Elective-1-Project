using System;
using UnityEngine;

namespace SceneController
{
    public class LoadSceneAfterTime : MonoBehaviour
    {
        public string scene;
        public float time = 2;

        private float timer;
        private bool called;

        private void Start()
        {
            timer = time;
        }

        public void Update()
        {
            timer -= Time.deltaTime;

            if (!(timer <= 0) || called) return;
            
            called = true;
            GlobalSceneController.loadSingleEvent?.Invoke(scene);
        }
    }
}