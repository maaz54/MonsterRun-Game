using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;
using Gameplay.UI;
using System.Threading.Tasks;

namespace Gameplay.Manager.UI
{
    public class UiManager : MonoBehaviour
    {
        [SerializeField] GameObject menuPanel;
        [SerializeField] GameObject GameplayPanel;
        [SerializeField] GameObject ResultPanel;
        [SerializeField] Button gameStartButton;
        [SerializeField] TextMeshProUGUI roundNumberText;
        [SerializeField] GameStartCounter gameStartCounter;

        public Action OnGameStartButton;

        private void Start()
        {
            gameStartButton.onClick.AddListener(GameStartButton);
        }

        public async Task StartGame(int roundNumber)
        {
            GameplayPanel.SetActive(true);
            menuPanel.SetActive(false);
            ResultPanel.SetActive(false);
            roundNumberText.text = "Round: " + roundNumber.ToString();
            await gameStartCounter.StartCounter(3);
        }

        private void GameStartButton()
        {
            OnGameStartButton?.Invoke();
        }


    }
}