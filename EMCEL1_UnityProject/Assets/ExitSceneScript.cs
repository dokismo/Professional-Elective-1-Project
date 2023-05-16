using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using Player.Control;
public class ExitSceneScript : MonoBehaviour
{
    private void Awake()
    {
        Destroy();
    }
    void Destroy()
    {
        Destroy(FindObjectOfType<PlayerUIHandler>().gameObject);
        Destroy(FindObjectOfType<Movement>().gameObject);
        Destroy(FindObjectOfType<DontDestroyOnLoad>().gameObject);
        Destroy(FindObjectOfType<WaveManagerScript>().gameObject);
    }

}
