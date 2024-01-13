using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

namespace Gameplay.Manager.UI
{
    public class UiManager : MonoBehaviour
    {
        [SerializeField] GameObject menuPanel;
        [SerializeField] GameObject GameplayPanel;
        [SerializeField] GameObject ResultPanel;
        [SerializeField] Button gameStartButton;
        [SerializeField] TextMeshProUGUI roundNumberText;

        public Action OnGameStartButton;

        private void Start()
        {
            gameStartButton.onClick.AddListener(GameStartButton);
        }

        private void GameStartButton()
        {
            OnGameStartButton?.Invoke();
        }

        public void StartGame(int roundNumber)
        {
            GameplayPanel.SetActive(true);
            menuPanel.SetActive(false);
            ResultPanel.SetActive(false);
            roundNumberText.text = "Round: " + roundNumber.ToString();
        }

    }
}