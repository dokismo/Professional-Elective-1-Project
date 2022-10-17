using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayMenuMusic : MonoBehaviour
{
    public AudioSource playBg;
    
    public void playBgm()
    {
        playBg.Play();
    }
}
