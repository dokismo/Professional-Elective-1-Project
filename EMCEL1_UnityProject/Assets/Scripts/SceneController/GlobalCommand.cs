using UnityEngine;

namespace SceneController
{
    public class GlobalCommand : MonoBehaviour
    {
        public delegate void Control(bool value);

        public static Control setPause;

        private void OnEnable()
        {
            setPause += SetPause;
        }

        private void OnDisable()
        {
            setPause -= SetPause;
        }
    
        private void SetPause(bool value)
        {
            Time.timeScale = value ? 0 : 1;
        }
    
    }
}