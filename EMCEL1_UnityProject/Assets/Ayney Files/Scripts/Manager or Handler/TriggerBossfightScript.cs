using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TriggerBossfightScript : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if(other.tag == "Player")
        {
            BossFightManager.Instance.StartBossFight();
            Destroy(gameObject);
        }
    }
}
