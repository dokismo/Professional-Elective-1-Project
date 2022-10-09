using System;
using CORE;
using UnityEngine;

namespace Environment.Map_Generation
{
    public class Rooms : MonoBehaviour
    {
        [SerializeField] private float openChance;
        public bool isOpen;
        private SpriteRenderer roomDoor;
        // Storage List

        private void Start()
        {
            GetReferences();
            // Find Storages
        }
        
        public void SetupRandom() => Setup(Utilities.RandomBool(openChance));

        public void Setup(bool isOpen)
        {
            GetReferences();
            this.isOpen = isOpen;
            
            if (!isOpen)
            {
                roomDoor.color = Utilities.ColorWithTransparencies(roomDoor.color, 1);
                
                // Close Storages
                return;
            }
            
            roomDoor.color = Utilities.ColorWithTransparencies(roomDoor.color, 0.5f);
            
            // Random Storages
        }

        private void GetReferences()
        {
            roomDoor ??= transform.GetChild(0).GetComponent<SpriteRenderer>();
        }
    }
}