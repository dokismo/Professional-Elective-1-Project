using System;
using System.Collections.Generic;
using SFX.Manager;
using UnityEngine;
using UnityEngine.UI;

namespace SFX.Main_Menu.Options
{
    public class VolumeController : MonoBehaviour
    {
        public List<Sprite> volumeSprites;
        public Image volumeImage;
        public string mixerName;

        [SerializeField]
        private int volume = 5;
        
        private int maxVolume = 10;
        
        public float Volume => Muted ? 0 : (float)volume / 10;
        public bool Muted => volume == 0;

        private void OnEnable()
        {
            SetValue(Mathf.RoundToInt(PlayerPrefs.GetFloat(mixerName, 1)));
        }

        private void OnDisable()
        {
            VolumeManager.save?.Invoke();
        }

        private void Start()
        {
            SetSprite();
        }

        public void AddBy(int value)
        {
            volume += value;
            volume = Mathf.Clamp(volume, 0, maxVolume);
            SetSprite();
            
            VolumeManager.volumeChangeSearch?.Invoke(mixerName, Volume);
        }

        public void SetValue(int value)
        {
            volume = Mathf.Clamp(value, 0, maxVolume);
            SetSprite();
            
            VolumeManager.volumeChangeSearch?.Invoke(mixerName, Volume);
        }

        private void SetSprite() => volumeImage.sprite = volumeSprites[volume];
    }
}
