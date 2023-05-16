using System;
using UnityEngine;
using UnityEngine.AI;

namespace Audio_Scripts
{
    [Serializable]
    public class NavAiWalkingSfx
    {
        public float interval = 0.3f;
        public AudioClip audioClip;

        public float timer;
    }

    [RequireComponent(typeof(NavMeshAgent), typeof(AudioSource))]
    public class EnemySounds : MonoBehaviour
    {
        public NavAiWalkingSfx navAiWalkingSfx;
        
        private NavMeshAgent navMeshAgent;
        private AudioSource audioSource;

        private void Start()
        {
            navMeshAgent = GetComponent<NavMeshAgent>();
            audioSource = GetComponent<AudioSource>();
        }

        private void Update()
        {
            Walking();
        }

        private void Walking()
        {
            if (navMeshAgent.velocity.magnitude <= 0.1) return;

            navAiWalkingSfx.timer = Mathf.Clamp01(navAiWalkingSfx.timer - Time.deltaTime);

            if (!(navAiWalkingSfx.timer <= 0)) return;
            
            navAiWalkingSfx.timer = navAiWalkingSfx.interval;
            audioSource.PlayOneShot(navAiWalkingSfx.audioClip);
        }
    }
}