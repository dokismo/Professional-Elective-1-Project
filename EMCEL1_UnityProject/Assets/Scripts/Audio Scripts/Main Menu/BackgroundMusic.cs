using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BackgroundMusic : MonoBehaviour
{
    public AudioSource track1, track2;

    public int TrackSelector;

    public int TrackHistory;
    void Start()
    {
        TrackSelector = Random.Range(0, 2);

        if (TrackSelector == 0)
        {
            track1.Play();
            TrackHistory = 1;
        }
        else if (TrackSelector == 1)
        {
            track2.Play();
            TrackHistory = 2;
        }
    }

    void Update()
    {
        if (track1.isPlaying == false && track2.isPlaying == false)
        {
            TrackSelector = Random.Range(0, 3);

            if (TrackSelector == 0 && TrackHistory !=1)
            {
                track1.Play();
                TrackHistory = 1;
            }
            else if (TrackSelector == 1 && TrackHistory != 2)
            {
                track2.Play();
                TrackHistory = 2;
            }
        }

    }
}
