using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using UnityEngine.SceneManagement;
using TMPro;
using UnityEngine.UI;
public class BossFightManager : MonoBehaviour
{
    public static BossFightManager Instance;

    public static event Action OnBossStart;

    public GameObject BossGameObject;
    public GameObject BossFightUI;
    public TextMeshProUGUI BossNameText;

    public bool BossFightStarted = false;
    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        DontDestroyOnLoad(gameObject);
    }

    private void OnEnable()
    {
        PlayerUIHandler.onMainMenu += DestroyThis;
        SceneManager.sceneLoaded += GetBossOnScene;
    }
    private void OnDisable()
    {
        PlayerUIHandler.onMainMenu -= DestroyThis;
        SceneManager.sceneLoaded -= GetBossOnScene;
    }


    public void AssignBossVar()
    {
        
        BossGameObject = GameObject.FindWithTag("Boss").gameObject;

        if(BossGameObject.GetComponent<BossHPHandler>() != null && BossFightUI != null)
        {
            BossGameObject.GetComponent<BossHPHandler>().BossHPBar = BossFightUI.transform.Find("Boss HP Bar").GetComponent<Slider>();
            BossGameObject.GetComponent<BossHPHandler>().BossHPBar.maxValue = BossGameObject.GetComponent<BossHPHandler>().BossHP;
            BossGameObject.GetComponent<BossHPHandler>().BossHPBar.value = BossGameObject.GetComponent<BossHPHandler>().BossHP;
            if(FindObjectOfType<Boss1Script>()) BossGameObject.GetComponent<Boss1Script>().Player = GameObject.FindGameObjectWithTag("Player").transform;
            else if (FindObjectOfType<Boss2Script>()) BossGameObject.GetComponent<Boss2Script>().Player = GameObject.FindGameObjectWithTag("Player").transform;
            
                
        }
        
    }

    public void StartBossFight()
    {
        PlayerUIHandler.Instance.DisableGameObject("Round Handler");
        PlayerUIHandler.Instance.EnableGameObject("Boss Fight UI");
        OnBossStart?.Invoke();
        BossFightStarted = true;
    }

    
    public void GetBossOnScene(Scene scene, LoadSceneMode mode)
    {
        BossGameObject = GameObject.FindWithTag("Boss");
        PlayerUIHandler.Instance?.AssignVariables();
        if (BossGameObject != null && BossNameText != null)
        {
            
            BossNameText.text = BossGameObject.name;
        }
            
        else Debug.Log("CAN'T FIND BOSS");
    }

    public void DestroyThis()
    {
        Destroy(gameObject);
    }
}
