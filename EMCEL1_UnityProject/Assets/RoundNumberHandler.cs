using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class RoundNumberHandler : MonoBehaviour
{
    public TextMeshProUGUI RoundAnim;
    public TextMeshProUGUI RoundNumberText;


    
    
    void Start()
    {
        GameObject.Find("For Wave Management").GetComponent<WaveDifficultyIncrement>().RoundNumAnim = GameObject.Find("RoundNumberAnim");
        RoundAnim = GameObject.Find("RoundNumberAnim").GetComponent<TextMeshProUGUI>();
        RoundNumberText = GameObject.Find("RoundNumber").GetComponent<TextMeshProUGUI>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void ChangeWaveNumber()
    {
        WaveDifficultyIncrement WDIScript = GameObject.FindObjectOfType<WaveDifficultyIncrement>();
        if (WDIScript != null)
        {
            RoundNumberText.text = "Wave " + WDIScript.WaveNumber;
            Debug.Log("CHANGED WAVE");
        }

    }
}
