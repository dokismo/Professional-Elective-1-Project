using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;

public class RoundNumberHandler : MonoBehaviour
{
    public TextMeshProUGUI RoundAnim;
    public TextMeshProUGUI RoundNumberText;

    private void OnEnable()
    {
        SceneManager.sceneLoaded += ResetWaveOnSceneLoad;
        //SceneManager.sceneLoaded += ChangeWaveNumberOnSceneLoad;
    }
    private void OnDisable()
    {
        
        //SceneManager.sceneLoaded -= ChangeWaveNumberOnSceneLoad;
    }


    private void Awake()
    {
        if(GameObject.Find("Game Manager") != null)
        {
            GameObject.Find("Game Manager").GetComponent<WaveManagerScript>().RoundNumAnim = gameObject;
            RoundAnim = GameObject.Find("RoundNumberAnim").GetComponent<TextMeshProUGUI>();
            RoundNumberText = GameObject.Find("RoundNumber").GetComponent<TextMeshProUGUI>();
        }
    }
    void Start()
    {
        //GameObject.Find("For Wave Management").GetComponent<WaveDifficultyIncrement>().RoundNumAnim = GameObject.Find("RoundNumberAnim");

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

    void ChangeWaveNumberOnSceneLoad(Scene sceneName, LoadSceneMode mode)
    {
        ChangeWaveNumber();
    }

    void ResetWaveOnSceneLoad(Scene sceneName, LoadSceneMode mode)
    {
        if(RoundNumberText!= null) RoundNumberText.text = "";
    }

    private void OnDestroy()
    {
        SceneManager.sceneLoaded -= ResetWaveOnSceneLoad;
    }
}
