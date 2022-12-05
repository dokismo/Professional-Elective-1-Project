using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using _2._5D_Objects;

public class BossAnimationHandler : MonoBehaviour
{
    SpriteRotation BossRotation;


    Animator BossAnimator;

    private void Start()
    {
        BossRotation = GetComponent<SpriteRotation>();
        BossAnimator = GetComponent<Animator>();
    }

    private void Update()
    {
        BossAnimator.SetFloat("X", BossRotation.Front * -1);


        
    }

}
