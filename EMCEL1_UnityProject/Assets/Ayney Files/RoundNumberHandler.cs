using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class RoundNumberHandler : MonoBehaviour
{
    public TextMeshProUGUI RoundAnim;
    public TextMeshProUGUI RoundNumberText;


    /// <summary>
    /// AYUSIN UNG PAG CHANGE NG WAVE.
    /// </summary>

    private void Awake()
    {
        GameObject.Find("Wave Manager").GetComponent<WaveManagerScript>().RoundNumAnim = GameObject.Find("RoundNumberAnim");
        RoundAnim = GameObject.Find("RoundNumberAnim").GetComponent<TextMeshProUGUI>();
        RoundNumberText = GameObject.Find("RoundNumber").GetComponent<TextMeshProUGUI>();
    }
    void Start()
    {
        //GameObject.Find("For Wave Management").GetComponent<WaveDifficultyIncrement>().RoundNumAnim = GameObject.Find("RoundNumberAnim");
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void ChangeWaveNumber()
    {
        WaveManagerScript WDIScript = GameObject.FindObjectOfType<WaveManagerScript>();
        if (WDIScript != null)
        {
            RoundNumberText.text = "Wave " + WDIScript.WaveNumber;
            Debug.Log("CHANGED WAVE");
        }

    }
}
