using System;
using UnityEngine;
using UnityEngine.UI;

namespace SFX.Main_Menu.Options
{
    public class Muter : MonoBehaviour
    {
        public Sprite onSprite, offSprite;
        public Image image;
        public VolumeController music, sfx;

        public bool on;

        public void Toggle()
        {
            on = !on;
            Refresh();
        }

        private void Update()
        {
            // CheckVolumes();
        }

        // private void CheckVolumes()
        // {
        //     float checker = music.Volume - sfx.Volume;
        //     if (checker > 0) return;
        //
        //     switch (on)
        //     {
        //         case false when checker == 0 && music.Volume == 0:
        //             on = false;
        //             Refresh();
        //             break;
        //         case true when checker > 0:
        //             on = true;
        //             Refresh();
        //             break;
        //     }
        // }

        private void Refresh()
        {
            image.sprite = on
                ? onSprite
                : offSprite;
            
            if (on)
            {
                music.Mute();
                sfx.Mute();
            }
            else
            {
                music.UnMute();
                sfx.UnMute();
            }
        }
    }
}