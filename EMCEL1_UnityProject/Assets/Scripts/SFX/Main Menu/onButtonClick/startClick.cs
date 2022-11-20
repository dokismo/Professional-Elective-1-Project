using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class startClick : MonoBehaviour
{
    void Start()
    {
        FindObjectOfType<AudioManager>().Play("clickButton");
    }

}
