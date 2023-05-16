using System;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

namespace UI.Loading
{
    public class StoryTelling : MonoBehaviour
    {
        public List<string> messages;
        public TextMeshProUGUI textMeshProUGUI;
        public GameObject next;

        private int currentIndex;

        private void Start()
        {
            SetText();
        }

        public void Next()
        {
            currentIndex++;
            SetText();
        }

        private void SetText()
        {
            gameObject.SetActive(messages.Count > currentIndex);
            next.SetActive(messages.Count <= currentIndex);

            textMeshProUGUI.text = messages.Count > currentIndex
                ? messages[currentIndex]
                : "...";
        }
    }
}