using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
public class PlayerUIHandler : MonoBehaviour
{
    public static PlayerUIHandler Instance;

    public delegate void OnRetry();
    public static event OnRetry onRetry;

    public delegate void OnMainMenu();
    public static event OnMainMenu onMainMenu;

    public GameObject[] UIObjects;

    public GameObject Manager;
    public GameObject GameManager;


    void Awake()
    {

        if (Instance == null)
        {
            Instance = this;
        }

        CheckForOtherInstance();

        UIObjects = new GameObject[transform.childCount];
        for (int i = 0; i < transform.childCount; i++)
        {
            UIObjects[i] = transform.GetChild(i).gameObject;

            //Assign disabled gameobject to boss fight manager variable.
            if (UIObjects[i].name == "Boss Fight UI" && BossFightManager.Instance != null)
            {
                BossFightManager.Instance.BossFightUI = UIObjects[i];
                BossFightManager.Instance.BossNameText = UIObjects[i].transform.Find("Boss Name").GetComponent<TextMeshProUGUI>();
            }

            if (UIObjects[i].name == "Loading Screen")
            {
                SceneLoader.Instance.LoadingScreen = UIObjects[i];
                SceneLoader.Instance.LoadingSlider = UIObjects[i].GetComponentInChildren<Slider>();
            }
        }
    }
    private void OnEnable()
    {
        SceneManager.sceneLoaded += ResetUIActiveForOnSceneLoadAndStartWaveNumAnim;
    }



    private void OnDisable()
    {
        SceneManager.sceneLoaded -= ResetUIActiveForOnSceneLoadAndStartWaveNumAnim;
    }

    void CheckForOtherInstance()
    {
        GameObject anotherUI = GameObject.Find("Canvas");
        if (anotherUI != null && anotherUI != this.gameObject)
        {
            Debug.Log("ANOTHER INSTANCE FOUND");
            Destroy(anotherUI);
        }
        DontDestroyOnLoad(this);
    }


    public void EnableGameObject(string name)
    {
        for (int i = 0; i < UIObjects.Length; i++)
        {
            if (UIObjects[i].name == name)
            {
                UIObjects[i].SetActive(true);
                break;
            }
        }
    }

    public void DisableGameObject(string name)
    {
        for (int i = 0; i < UIObjects.Length; i++)
        {
            if (UIObjects[i].name == name)
            {
                UIObjects[i].SetActive(false);
                break;
            }
        }
    }
    public void ResetManagers()
    {
        Destroy(GameObject.Find("Game Manager"));
        Instantiate(Manager, Vector3.zero, Quaternion.identity);
        Instantiate(GameManager, Vector3.zero, Quaternion.identity);
    }

    public void AssignVariables()
    {
        for (int i = 0; i < UIObjects.Length; i++)
        {
            //Assign disabled gameobject to boss fight manager variable.
            if (UIObjects[i] != null && UIObjects[i].name == "Boss Fight UI")
            {
                BossFightManager.Instance.BossFightUI = UIObjects[i];
                BossFightManager.Instance.BossNameText = UIObjects[i].transform.Find("Boss Name").GetComponent<TextMeshProUGUI>();
            }

            if (UIObjects[i] != null && UIObjects[i].name == "Loading Screen")
            {
                SceneLoader.Instance.LoadingScreen = UIObjects[i];
                SceneLoader.Instance.LoadingSlider = UIObjects[i].GetComponentInChildren<Slider>();
            }
        }
        BossFightManager.Instance?.AssignBossVar();
    }

    public void ResetUIActiveForOnSceneLoadAndStartWaveNumAnim(Scene sceneName, LoadSceneMode mode)
    {
        ResetUIActive();
        WaveManagerScript.Instance?.StartWaveNumChange();
    }
    public void ResetUIActive()
    {
        for (int i = 0; i < UIObjects.Length; i++)
        {
            if (UIObjects[i].name == "Boss Fight UI") UIObjects[i].SetActive(false);
            else if (UIObjects[i].name == "Paused") UIObjects[i].SetActive(false);
            else if (UIObjects[i].name == "Options Menu") UIObjects[i].SetActive(false);
            else if (UIObjects[i].name == "GameOver") UIObjects[i].SetActive(false);
            else if (UIObjects[i].name == "Loading Screen") return;
            else UIObjects[i].SetActive(true);
        }
    }

    void ResetRoundNum()
    {
        for (int i = 0; i < UIObjects.Length; i++)
        {
            if (UIObjects[i].name == "Round Handler")
            {
                UIObjects[i].transform.Find("RoundNumber").GetComponent<TextMeshProUGUI>().text = "";
            }
        }
    }
    public void Retry()
    {
        ResetManagers();
        ResetUIActive();
        ResetRoundNum();

        onRetry?.Invoke();
    }

    public void MainMenuButton()
    {
        onMainMenu?.Invoke();
        Destroy(gameObject);
    }

}

