using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UI.PlayerScreen;
public class TriggerBossfightScript : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if(other.tag == "Player")
        {
            if (FindObjectOfType<Boss2Script>()) FindObjectOfType<Boss2Script>().IsScreaming = true;
            BossFightManager.Instance.StartBossFight();
            FindObjectOfType<DisplayStatus>().SendMessageToPlayer("YOU HAVE AWAKEN \n HIM FROM HIS \n DEEP SLUMBER!", Color.red);
            Destroy(gameObject);
        }
    }
}
