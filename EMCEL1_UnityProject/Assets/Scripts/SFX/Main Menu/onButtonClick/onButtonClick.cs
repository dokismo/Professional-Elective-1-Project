using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class onButtonClick : MonoBehaviour
{

    public AudioSource onClickSound;
    
    public void playThisSoundEffect()
    {
        onClickSound.Play();
    }
}
