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

    void Start()
    {
    }

    
    public void AssignBossVar()
    {
        PlayerUIHandler.Instance?.AssignVariables();
        BossGameObject = GameObject.FindWithTag("Boss").gameObject;
        BossNameText.text = BossGameObject.name;
        BossGameObject.GetComponent<BossHPHandler>().BossHPBar = BossFightUI.transform.Find("Boss HP Bar").GetComponent<Slider>();
        BossGameObject.GetComponent<BossHPHandler>().BossHPBar.maxValue = BossGameObject.GetComponent<BossHPHandler>().BossHP;
        BossGameObject.GetComponent<BossHPHandler>().BossHPBar.value = BossGameObject.GetComponent<BossHPHandler>().BossHP;
        BossGameObject.GetComponent<Boss1Script>().Player = GameObject.FindGameObjectWithTag("Player").transform;
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

        if (BossGameObject != null)
        {
            BossNameText.text = BossGameObject.name;

            

            Debug.Log("Boss found!");
        }
            


        else Debug.Log("CAN'T FIND BOSS");
    }


}
