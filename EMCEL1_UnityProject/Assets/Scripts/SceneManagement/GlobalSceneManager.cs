using UnityEngine;

namespace SceneManagement
{
    public class GlobalSceneManager : MonoBehaviour
    {
        [SerializeField] private SceneScriptableObject sceneScriptableObject;

        private void Start()
        {
            sceneScriptableObject.GoToMainMenu();
        }

        private void OnApplicationQuit()
        {
            sceneScriptableObject.CurrentMainScene = null;
            
            foreach (var localGroupScene in sceneScriptableObject.mainSceneGroup)
            {
                localGroupScene.UnloadAll();
            }
        }
    }
}