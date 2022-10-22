using System;
using System.Collections.Generic;
using SceneManagement;
using UnityEngine;
using Random = System.Random;

namespace Maps
{
    [Serializable]
    public class Locations
    {
        private readonly Random _random = new Random();
        
        public List<string> locationList;
        public string currentLocation;
        
        public string RandomLocation()
        {
            currentLocation = locationList[_random.Next(0, locationList.Count)];
            return currentLocation;
        }
    }
    
    public class MapLoader : MonoBehaviour
    {
        public Locations locationA;

        private void Start()
        {
            GlobalSceneManager.LoadAdditiveScene?.Invoke(locationA.RandomLocation());
        }

        private void Update()
        {
            if (Input.GetKeyDown(KeyCode.M))
                ReplaceScene();
        }

        private void ReplaceScene()
        {
            GlobalSceneManager.UnloadAdditiveScene?.Invoke(locationA.currentLocation);
            GlobalSceneManager.LoadAdditiveScene?.Invoke(locationA.RandomLocation());
        }
    }
}