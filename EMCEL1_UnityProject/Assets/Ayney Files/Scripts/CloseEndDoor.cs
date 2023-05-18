using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CloseEndDoor : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if(other.tag == "Player")
        {
            GameObject.Find("End Door").GetComponent<Animator>().SetBool("PlayerPassed", true);
        }
    }
}
