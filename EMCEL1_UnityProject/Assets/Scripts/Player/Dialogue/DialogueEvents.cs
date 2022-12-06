using System;
using Shop;
using UnityEngine;
using Random = UnityEngine.Random;

namespace Player.Dialogue
{
    public class DialogueEvents : MonoBehaviour
    {
        public static Action firstBlood;
        public static Action firstBossKilled;
        public static Action lowHealth;
        public static Action boughtGun;
        public static Action randomDialogue;

        public Vector2 randomDialogueInterval = new(12f, 25f);

        private float timer;
        private bool isFirstBlood = true, isFirstBossKilled = true, onZombieSpawn, onStarted = true;

        private void OnEnable()
        {
            EnemyHpHandler.OnTheDeath += ZombieDeath;
            ZombieBossScript.onBossDeath += OnBossDeath;
            WallShop.itemBought += ItemBought;
            PlayerStatusScriptable.playerLowHealth += PlayerLowHealth;
        }

        private void OnDisable()
        {
            EnemyHpHandler.OnTheDeath -= ZombieDeath;
            ZombieBossScript.onBossDeath -= OnBossDeath;
            WallShop.itemBought -= ItemBought;
            PlayerStatusScriptable.playerLowHealth -= PlayerLowHealth;
        }

        private void Start()
        {
            timer = Random.Range(randomDialogueInterval.x, randomDialogueInterval.y);
        }

        private void Update()
        {
            SetTimer();
        }

        private void SetTimer()
        {
            timer -= Time.deltaTime;

            if (!(timer <= 0)) return;
            
            timer = Random.Range(randomDialogueInterval.x, randomDialogueInterval.y);
            randomDialogue?.Invoke();
        }

        private void ZombieDeath()
        {
            if (!isFirstBlood) return;

            isFirstBlood = false;
            firstBlood?.Invoke();
        }

        private void OnBossDeath()
        {
            if (!isFirstBossKilled) return;

            isFirstBossKilled = false;
            firstBossKilled?.Invoke();
        }

        private void PlayerLowHealth()
        {
            lowHealth?.Invoke();
        }

        private void ItemBought() => boughtGun?.Invoke();
    }
}