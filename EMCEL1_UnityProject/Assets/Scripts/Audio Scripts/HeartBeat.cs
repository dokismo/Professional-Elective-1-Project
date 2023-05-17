using System;
using Player;
using UnityEngine;
using UnityEngine.Audio;

namespace Audio_Scripts
{
    public class HeartBeat : MonoBehaviour
    {
        [SerializeField]
        private AudioMixer audioMixer;
        
        [SerializeField] 
        private PlayerStatusScriptable playerStatusScriptable;
        
        [SerializeField]
        private float beatPercent = 0.3f;

        [SerializeField] 
        private float startPitch = 0.3f;
        
        
        private const string AudioMixerParam = "HeartBeatPitch";
        private float Health => playerStatusScriptable.health;
        private float MaxHealth => playerStatusScriptable.maxHealth;
        public float HealthPercent => Health / MaxHealth;
        private bool InRange => HealthPercent <= beatPercent;
        private float Pitch => startPitch + Mathf.Lerp(0, 1f, audioSource.volume);
        
        private AudioSource audioSource;

        private void Start()
        {
            audioSource = GetComponent<AudioSource>();
        }

        private void Update()
        {
            audioSource.enabled = InRange;

            if (!InRange) return;

            audioSource.volume = Mathf.Lerp(1, 0, HealthPercent);
            audioMixer.SetFloat(AudioMixerParam, Pitch);
        }
    }
}