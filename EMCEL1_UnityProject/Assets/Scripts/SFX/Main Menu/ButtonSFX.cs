using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ButtonSFX : MonoBehaviour
{
    public AudioSource audiosource;
    public AudioClip clickSelect, clickBack, clickRun, clickVolume, clickMute, charHover;

    public void onClick()
    {
        audiosource.clip = clickSelect;
        audiosource.Play();
    }
    public void onClickBack()
    {
        audiosource.clip = clickBack;
        audiosource.Play();
    }
    public void onClickRun()
    {
        audiosource.clip = clickRun;
        audiosource.Play();
    }
    public void onClickTick()
    {
        audiosource.clip = clickVolume;
        audiosource.Play();
    }
    public void onClickMute()
    {
        audiosource.clip = clickMute;
        audiosource.Play();
    }
    public void onCharHover()
    {
        audiosource.clip = charHover;
        audiosource.Play();
    }
}
