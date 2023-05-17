using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NextSceneNameHolder : MonoBehaviour
{
    public static NextSceneNameHolder Instance;

    private void Awake()
    {
        if(Instance == null)
        {
            Instance = this;
        }
    }
    public string NextSceneName;
}
