using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace SFX.Main_Menu.Options
{
    public class VolumeController : MonoBehaviour
    {
        public List<Sprite> volumeSprites;
        public Image volumeImage;

        [SerializeField]
        private int volume = 5;
        
        private int maxVolume = 10;
        
        // Use this juswa to get the value
        public float Volume => Muted ? 0 : (float)volume / 10;
        public bool Muted => volume == 0;

        private void Start()
        {
            SetSprite();
        }

        public void AddBy(int value)
        {
            volume += value;
            volume = Mathf.Clamp(volume, 0, maxVolume);
            SetSprite();
        }

        private void SetSprite() => volumeImage.sprite = volumeSprites[volume];

        public void Mute()
        {
            volume = 0;
            SetSprite();
        }

        public void UnMute()
        {
            volume = 1;
            SetSprite();
        }
    }
}
