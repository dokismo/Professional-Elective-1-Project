using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;

public class WaveManagerScript : MonoBehaviour
{
    public static WaveManagerScript Instance;

    [Header("           References")]

    public GameObject RoundNumAnim;
    TextMeshProUGUI RoundNumText;
    [SerializeField] NEWSpawningScript SpawningScript;
    

    [Header("           Variables")]

    public int WaveNumber = 0;
    bool EndOfRound;
    [SerializeField] float RestTime, DEFAULTRestTime;

    [Header("           Difficulty Stats Multiplier")]

    public float HPMultiplier = 0;
    public float DMGMultiplier = 0;


    private void OnEnable()
    {
        PlayerUIHandler.onMainMenu += DestroyThis;
        BossFightManager.OnBossStart += DisableWaveText;
    }

    private void OnDisable()
    {
        PlayerUIHandler.onMainMenu -= DestroyThis;
        BossFightManager.OnBossStart -= DisableWaveText;
    }


    private void Awake()
    {
        if(Instance == null)
        {
            Instance = this;
        }

        RoundNumAnim = GameObject.Find("RoundNumberAnim");
        
        DEFAULTRestTime = RestTime + 1;
    }

    private void Start()
    {
        RoundNumText = RoundNumAnim.GetComponent<RoundNumberHandler>().RoundNumberText;
        SpawningScript = GetComponent<NEWSpawningScript>();
        RoundStart();
    }

    


    void Update()
    {
        
        if (SpawningScript.ZombieDead == SpawningScript.MaxZombieToSpawn && !EndOfRound)
        {
            RoundEnd();
        }

        if (EndOfRound)
        {
            RestTimer();
        }
    }

    public void RoundStart()
    {

        WaveNumber++;
        SpawningScript.MaxZombieToSpawn += WaveNumber;
        SpawningScript.CanSpawn = true;

        StartWaveNumChange();
    }
    public void StartWaveNumChange()
    {
        RoundNumAnim.GetComponent<TextMeshProUGUI>().text = "Wave " + WaveNumber;
        RoundNumAnim.GetComponent<Animator>().Play("Change Number", 0, 0);
    }


    public void RoundEnd()
    {
        RestTime = DEFAULTRestTime;
        EndOfRound = true;
        SpawningScript.ZombieDead = 0;
        SpawningScript.ZombiesSpawned = 0;

    }

    void RestTimer()
    {
        if (RestTime > 0)
        {
            RestTime -= Time.deltaTime;

            RoundNumText.text = "Wave " + WaveNumber + "\n NEXT WAVE IN " + (int)RestTime;
        }
        else
        {
            RoundNumText.text = "Wave " + WaveNumber;
            RoundStart();
            EndOfRound = false;
        }
    }

    void DisableWaveText()
    {
        RoundNumAnim.transform.parent.gameObject.SetActive(false);
    }

    void EnableWaveText()
    {
        RoundNumAnim.transform.parent.gameObject.SetActive(true);
    }

    public void DestroyThis()
    {
        Destroy(gameObject);
    }
}
