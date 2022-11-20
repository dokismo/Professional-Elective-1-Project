using System.Collections.Generic;
using UnityEngine;

namespace SceneController
{
    [CreateAssetMenu(fileName = "Scenes List", menuName = "Scenes List")]
    public class ScenesListScriptable : ScriptableObject
    {
        public List<SceneGroup> sceneGroups;

        public List<SceneGroup> SceneGroups => sceneGroups;
    }
}