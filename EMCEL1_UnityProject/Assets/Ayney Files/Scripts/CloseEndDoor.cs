using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UI.PlayerScreen;
public class CloseEndDoor : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if(other.tag == "Player")
        {
            GameObject.Find("End Door").GetComponent<Animator>().SetBool("PlayerPassed", true);
            GameObject.Find("Enemy").SetActive(false);
            Destroy(gameObject);
            FindObjectOfType<NEWSpawningScript>().BossStartFunc();
            FindObjectOfType<DisplayStatus>().SendMessageToPlayer("MAY THE GODS \n MAKE YOUR DEATH \n QUICK AND PAINLESS", Color.red);
        }
    }
}
