using SceneController;
using UnityEngine;

namespace UI
{
    public class UITimeControl : MonoBehaviour
    {
        public void Pause() => GlobalCommand.setPause?.Invoke(true);

        public void UnPause() => GlobalCommand.setPause?.Invoke(false);
    }
}