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
        [SerializeField] UiResultScreen ResultPanel;
        [SerializeField] Button gameStartButton;
        [SerializeField] Button startNextRoundButton;
        [SerializeField] TextMeshProUGUI roundNumberText;
        [SerializeField] GameStartCounter gameStartCounter;
        [SerializeField] UiTimer uiTimer;
        public Action OnGameStartButton;
        public Action OnStartNextRoundButton;

        private void Start()
        {
            gameStartButton.onClick.AddListener(GameStartButton);
            startNextRoundButton.onClick.AddListener(StartNextRoundButton);

            menuPanel.SetActive(true);
            GameplayPanel.SetActive(false);
            ResultPanel.gameObject.SetActive(false);
        }

        public async Task StartGame(int roundNumber)
        {
            GameplayPanel.SetActive(true);
            menuPanel.SetActive(false);
            ResultPanel.gameObject.SetActive(false);
            roundNumberText.text = "Round: " + roundNumber.ToString();
            await gameStartCounter.StartCounter(3);
            _ = uiTimer.StartTimer();
        }

        public void StopTimer(out float timeElapsed)
        {
            uiTimer.StopTimer(out timeElapsed);
        }

        public void ShowResult(string[] PlayersRanking, string totalTime)
        {
            GameplayPanel.SetActive(false);
            menuPanel.SetActive(false);
            ResultPanel.gameObject.SetActive(true);
            ResultPanel.ShowResult(PlayersRanking, totalTime);
        }

        private void GameStartButton()
        {
            OnGameStartButton?.Invoke();
        }

        private void StartNextRoundButton()
        {
            OnStartNextRoundButton?.Invoke();
        }



    }
}