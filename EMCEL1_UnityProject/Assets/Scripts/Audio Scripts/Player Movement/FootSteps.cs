using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Player.Control
{
    public class FootSteps : MonoBehaviour
    {
        private Movement playerMovement;
        public GameObject walkStep;
        public GameObject runningStep;


        void Start()
        {
            Debug.Log("start sfx");
            walkStep.SetActive(false);
            runningStep.SetActive(false);
            playerMovement = GetComponent<Movement>();
        }

        void Update()
        {
            if (playerMovement.IsMoving)
            {
                walkStep.SetActive(true);
                Debug.Log("WALK SFX");
            }
            if (playerMovement.IsRunning)
            {
                walkStep.SetActive(false);
                runningStep.SetActive(true);
                Debug.Log("RUN SFX");
            }
            else if (!playerMovement.IsMoving)
            {
                walkStep.SetActive(false);
                runningStep.SetActive(false);
            }
        }
    }
}

