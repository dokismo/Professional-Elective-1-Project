using UnityEngine;
using UnityEngine.UI;

public class onClickContinue : MonoBehaviour
{
    public AudioSource audiosource;
    public AudioClip startContinue;
    public Button ButtonContinue;

    void Start()
    {
        Button btn = ButtonContinue.GetComponent<Button>();
        btn.onClick.AddListener(onClick);
    }
    public void onClick()
    {
        audiosource.PlayOneShot(startContinue);
    }
}
