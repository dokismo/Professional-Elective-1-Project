using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ButtonSFX : MonoBehaviour
{
    public AudioSource audiosource;
    public AudioClip clickSelect, clickBack, clickRun, clickVolume, clickMute, charHover;

    public void onClick()
    {
        audiosource.PlayOneShot(clickSelect);
    }
    public void onClickBack()
    {
        audiosource.PlayOneShot(clickBack);
    }
    public void onClickRun()
    {
        audiosource.PlayOneShot(clickRun);
    }
    public void onClickTick()
    {
        audiosource.PlayOneShot(clickVolume);
    }
    public void onClickMute()
    {
        audiosource.PlayOneShot(clickMute);
    }
    public void onCharHover()
    {
        audiosource.PlayOneShot(charHover);
    }
}
