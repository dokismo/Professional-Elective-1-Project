using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlaySoundButton : MonoBehaviour
{
    public AudioSource playSound;

    public void playButtonfx()
    {
        playSound.Play();
    }
}
