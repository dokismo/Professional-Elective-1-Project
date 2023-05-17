using System;
using SFX.Manager;
using UnityEngine;
using UnityEngine.UI;

namespace SFX.Main_Menu.Options
{
    public class Muter : MonoBehaviour
    {
        public Sprite onSprite, offSprite;
        public Image image;

        public bool on;

        private void Start()
        {
            int i = PlayerPrefs.GetInt(VolumeManager.MixerMute, 0);

            on = i == 1;
            Refresh();
        }

        public void Toggle()
        {
            on = !on;
            VolumeManager.muteToggle?.Invoke(on);
            Refresh();
        }

        private void Refresh()
        {
            image.sprite = on
                ? onSprite
                : offSprite;
        }
    }
}