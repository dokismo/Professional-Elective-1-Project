using System;
using System.Collections.Generic;
using UnityEngine;
using Random = System.Random;

namespace Player.Dialogue
{
    [CreateAssetMenu(fileName = "Character Dialogue", menuName = "Character/Dialogues")]
    public class DialogueScriptable : ScriptableObject
    {
        public string onSpawn = "";
        public string onKill = "";
        public string onBossKill = "";
        public string lowHealth = "";
        public string boughtGun = "";

        public List<string> randomMessages;

        public string GetRandomMsg()
        {
            Random rnd = new Random();

            return randomMessages.Count == 0 ? "" : randomMessages[rnd.Next(randomMessages.Count)];
        }
    }
}