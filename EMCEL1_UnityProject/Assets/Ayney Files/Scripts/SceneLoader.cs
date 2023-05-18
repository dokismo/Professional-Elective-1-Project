using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using SceneController;
using UnityEngine.UI;
public class SceneLoader : MonoBehaviour
{
    public static SceneLoader Instance { get; private set; }

    public Slider LoadingSlider;
    public GameObject LoadingScreen;


    private void Awake()
    {
        if(Instance == null)
        {
            Instance = this;
        }
        DontDestroyOnLoad(this);
    }
   
    public void NextScene()
    {
        PlayerUIHandler.Instance.EnableGameObject("Loading Screen");
        LoadingScreen.GetComponent<Animator>().Play("Open");
    }

    public void DoneLoading()
    {
        LoadingScreen.GetComponent<Animator>().Play("Close");
    }

    public IEnumerator LoadAsynch(string SceneName)
    {
        AsyncOperation LoadingOperation = SceneManager.LoadSceneAsync(SceneName);
        
        while (!LoadingOperation.isDone)
        {
            float Progress = Mathf.Clamp01(LoadingOperation.progress / 0.9f);
            LoadingSlider.value = Progress;

            yield return null;
        }

        if(LoadingOperation.isDone)
        {
            DoneLoading();
        }
        yield break;
    }


}
