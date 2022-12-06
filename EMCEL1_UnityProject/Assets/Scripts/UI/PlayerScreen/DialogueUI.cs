using System;
using Player.Dialogue;
using SceneController;
using TMPro;
using UnityEngine;

namespace UI.PlayerScreen
{
    public class DialogueUI : MonoBehaviour
    {
        public DialogueScriptable dialogueScriptable;

        public TextMeshProUGUI text;

        public float timeDisplayed = 3;

        private float displayTimer;
        private bool toggle;

        private void OnEnable()
        {
            DialogueEvents.firstBlood += FirstBlood;
            DialogueEvents.firstBossKilled += FirstBossKilled;
            DialogueEvents.lowHealth += LowHealth;
            DialogueEvents.boughtGun += BoughtGun;
            DialogueEvents.randomDialogue += RandomDialogue;
        }

        private void OnDisable()
        {
            DialogueEvents.firstBlood -= FirstBlood;
            DialogueEvents.firstBossKilled -= FirstBossKilled;
            DialogueEvents.lowHealth -= LowHealth;
            DialogueEvents.boughtGun -= BoughtGun;
            DialogueEvents.randomDialogue -= RandomDialogue;
        }

        private void Awake()
        {
            dialogueScriptable = InformationHolder.instance.dialogueScriptable;
        }

        private void Start()
        {
            OnSpawn();
        }

        private void Update()
        {
            displayTimer = Mathf.Clamp(displayTimer - Time.deltaTime, 0, timeDisplayed);
            text.color = Color.Lerp(Color.clear, Color.white, Mathf.Clamp01(displayTimer));
        }

        private void RandomDialogue() => Toast(dialogueScriptable.GetRandomMsg());
        private void BoughtGun() => Toast(dialogueScriptable.boughtGun);
        private void LowHealth() => Toast(dialogueScriptable.lowHealth);
        private void FirstBossKilled() => Toast(dialogueScriptable.onBossKill);
        private void OnSpawn() => Toast(dialogueScriptable.onSpawn);
        private void FirstBlood() => Toast(dialogueScriptable.onKill);

        private void Toast(string msg)
        {
            text.text = msg;
            text.color = Color.white;
            displayTimer = timeDisplayed;
        }
    }
}