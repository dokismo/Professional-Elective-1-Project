using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OnEnemyInteract : MonoBehaviour
{
    private EnemyHpHandler onEnemyStatus;

    public delegate void EnemyEvent();
    public static EnemyEvent gruntEvent, deathEvent;

    public AudioClip grunt, died;
    public AudioSource audiosource;
    private void OnEnable()
    {
        gruntEvent += zombieGrunt;
        deathEvent += zombieDied;
    }
    private void OnDisable()
    {
        gruntEvent -= zombieGrunt;
        deathEvent -= zombieDied;
    }
    private void Start()
    {
        onEnemyStatus = GetComponent<EnemyHpHandler>();
    }
    private void zombieGrunt()
    {
        audiosource.PlayOneShot(grunt);
    }
    private void zombieDied()
    {
        Debug.Log("Died Zombs");
        audiosource.clip = died;
        audiosource.Play();
    }
}


