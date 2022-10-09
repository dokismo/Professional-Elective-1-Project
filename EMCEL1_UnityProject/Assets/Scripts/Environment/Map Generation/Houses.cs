using System;
using System.Collections.Generic;
using System.Linq;
using CORE;
using UnityEngine;

namespace Environment.Map_Generation
{
    public class Houses : MonoBehaviour
    {
        [SerializeField] private float openChance = 50f;
        
        private bool isOpen;
        private SpriteRenderer houseDoor;
        private List<Rooms> roomsList;

        private void Start()
        {
            GetReferences();
            SetupRandom();
        }

        private void Update()
        {
            if (Input.GetKeyDown(KeyCode.A))
                SetupRandom();
        }
        
        private void SetupRandom() => Setup(Utilities.RandomBool(openChance));

        public void Setup(bool isOpen)
        {
            GetReferences();
            
            this.isOpen = isOpen;
            
            if (!isOpen)
            {
                // Call door to close
                houseDoor.color = Utilities.ColorWithTransparencies(houseDoor.color, 1);
                foreach (var rooms in roomsList) rooms.Setup(false);
                return;
            }
            
            // Call door to open
            houseDoor.color = Utilities.ColorWithTransparencies(houseDoor.color, 0.5f);
            foreach (var rooms in roomsList) rooms.SetupRandom();
        }

        private void GetReferences()
        {
            houseDoor ??= transform.GetChild(0).GetComponent<SpriteRenderer>();
            roomsList ??= transform.GetComponentsInChildren<Rooms>().ToList();
        }
    }
}
