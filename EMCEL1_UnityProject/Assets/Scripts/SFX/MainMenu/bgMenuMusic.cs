using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class bgMenuMusic : MonoBehaviour
{
    public static bgMenuMusic instance;

    private void Awake()
    {
        DontDestroyOnLoad(this.gameObject);

        if (instance == null)
        {
            instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }
}
