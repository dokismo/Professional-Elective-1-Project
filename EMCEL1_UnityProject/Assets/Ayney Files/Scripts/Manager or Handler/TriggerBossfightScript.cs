using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TriggerBossfightScript : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if(other.tag == "Player")
        {
            if (FindObjectOfType<Boss2Script>()) FindObjectOfType<Boss2Script>().IsScreaming = true;
            BossFightManager.Instance.StartBossFight();
            Destroy(gameObject);
        }
    }
}
