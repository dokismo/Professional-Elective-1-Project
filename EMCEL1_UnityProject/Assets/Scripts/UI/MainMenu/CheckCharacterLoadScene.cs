using System;
using Player;
using UnityEngine;

namespace UI.MainMenu
{
    public class CheckCharacterLoadScene : MonoBehaviour
    {
        public PlayerStatusScriptable playerStatusScriptable;
        
        private UILoadScene uiLoadScene;

        private void Start()
        {
            uiLoadScene = GetComponent<UILoadScene>();
        }

        public void CheckThenLoadSingle()
        {
            if (playerStatusScriptable.runtimeAnimatorController == null)
                return;
            
            uiLoadScene.LoadSingle();
        }
    }
}
