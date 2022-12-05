using System;
using Player;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using Image = UnityEngine.UI.Image;

namespace UI.MainMenu
{
    public class CharacterSelect : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler, IPointerClickHandler
    {
        public delegate void CharacterSelectEvent(CharacterSelect sender);
        public static CharacterSelectEvent selectEvent;
        
        public PlayerStatusScriptable playerStatusScriptable;
        public Animator animator;

        public Image image;

        private bool selected;
        private static readonly int Play = Animator.StringToHash("Play");

        private void OnEnable()
        {
            selectEvent += Deselect;
        }

        private void OnDisable()
        {
            selectEvent -= Deselect;
        }

        private void Deselect(CharacterSelect sender)
        {
            if (sender == this) return;

            Deselect();
        }

        private void Start()
        {
            playerStatusScriptable.RemoveAnimatorController();
            Deselect();
        }

        private void Deselect()
        {
            selected = false;
            image.color = Color.Lerp(Color.clear, Color.black, 0f);
        }
        
        private void Select()
        {
            if (selected) return;

            CharacterSelectSFX.selectCharEvent?.Invoke();//SFX
            selected = true;
            image.color = Color.Lerp(Color.clear, Color.black, 1f);
            playerStatusScriptable.SetAnimatorController(animator.runtimeAnimatorController);
            selectEvent?.Invoke(this);
        }


        public void OnPointerEnter(PointerEventData eventData)
        {
            if (selected) return;

            animator.SetTrigger(Play);
            image.color = Color.Lerp(Color.clear, Color.black, 0.7f);
        }

        public void OnPointerExit(PointerEventData eventData)
        {
            if (selected) return;

            image.color = Color.Lerp(Color.clear, Color.black, 0f);
        }

        public void OnPointerClick(PointerEventData eventData)
        {
            if (selected)
            {
                playerStatusScriptable.RemoveAnimatorController();
                Deselect();
            }
            else
                Select();
        }
    }
}