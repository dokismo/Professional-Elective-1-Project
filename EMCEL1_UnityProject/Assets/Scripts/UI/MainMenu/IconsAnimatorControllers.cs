using UnityEngine;

namespace UI.MainMenu
{
    [CreateAssetMenu(fileName = "IconScriptable", menuName = "UI/Icon Scriptable")]
    public class IconsAnimatorControllers : ScriptableObject
    {
        public RuntimeAnimatorController
            karl,
            judy,
            rebecca,
            rouper;

        public RuntimeAnimatorController GetController(string name)
        {
            name = name.ToUpper();
            
            return name switch
            {
                "KARL" => karl,
                "JUDY" => judy,
                "REBECCA" => rebecca,
                "ROUPER" => rouper,
                _ => null
            };
        }
    }
}