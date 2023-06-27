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
            walkStep.SetActive(false);
            runningStep.SetActive(false);
            playerMovement = GetComponent<Movement>();
        }

        void Update()
        {
            runningStep.SetActive(playerMovement.isGrounded && playerMovement.IsRunning);
            walkStep.SetActive(playerMovement.isGrounded && playerMovement.IsMoving && !playerMovement.IsRunning);
        }

       
    }
}

