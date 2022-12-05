using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace UI.MainMenu
{
    public class CharacterSelectSFX : MonoBehaviour
    {
        public delegate void CharSelectEvent();
        public static CharSelectEvent selectCharEvent;

        public AudioClip charSelect;
        public AudioSource audiosource;

        private CharacterSelect selectCharacter;

        private void OnEnable()
        {
            selectCharEvent += Selected;
        }
        private void OnDisable()
        {
            selectCharEvent -= Selected;
        }

        private void Start()
        {
            selectCharacter = GetComponent<CharacterSelect>(); 
        }

        // Update is called once per frame
        private void Selected()
        {
            audiosource.PlayOneShot(charSelect);
        }
    }
}

