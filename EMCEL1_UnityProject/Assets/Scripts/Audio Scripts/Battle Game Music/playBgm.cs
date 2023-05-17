using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class playBgm : MonoBehaviour
{
    void Start()
    {
        FindObjectOfType<SFXManager>().Play("bgm01");
    }

}
