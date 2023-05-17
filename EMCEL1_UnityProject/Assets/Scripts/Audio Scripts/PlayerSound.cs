using System;
using Player;
using Player.Control;
using UI.PlayerScreen;
using UnityEngine;

namespace Audio_Scripts
{
    public class PlayerSound : MonoBehaviour
    {
        public AudioSource audioSource;

        public AudioClip takeDamage, death, swapGun, heal;

        private bool firstTimeSwap = true;
        private bool onlyPlayDeathOnce;

        private void OnEnable()
        {
            DisplayStatus.onDead += PlayDead;
            PlayerStatusScriptable.playerTakeDamage += PlayDamage;
            PlayerStatus.setItemEvent += SwapItem;
            PlayerStatus.heal += PlayHeal;
        }

        private void OnDisable()
        {
            DisplayStatus.onDead -= PlayDead;
            PlayerStatusScriptable.playerTakeDamage -= PlayDamage;
            PlayerStatus.setItemEvent -= SwapItem;
            PlayerStatus.heal -= PlayHeal;
        }

        private void SwapItem()
        {
            if (firstTimeSwap)
            {
                firstTimeSwap = false;
                return;
            }
            
            audioSource.PlayOneShot(swapGun);
        }

        private void PlayDead()
        {
            if (onlyPlayDeathOnce) return;

            onlyPlayDeathOnce = true;
            audioSource.PlayOneShot(takeDamage);
        }

        private void PlayDamage()
        {
            audioSource.PlayOneShot(death);
        }

        private void PlayHeal()
        {
            audioSource.PlayOneShot(heal);
        }
    }
}