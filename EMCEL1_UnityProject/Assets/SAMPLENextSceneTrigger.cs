using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SAMPLENextSceneTrigger : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if(other.tag == "Player")
        {
            NextSceneNameHolder.Instance.NextSceneName = "NextSceneSample";
            SceneLoader.Instance.NextScene();
        }
    }
}
