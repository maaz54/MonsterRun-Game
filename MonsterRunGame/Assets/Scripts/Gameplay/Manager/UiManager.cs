using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using Gameplay.UI;
using System.Threading.Tasks;
using Gameplay.Interface;

namespace Gameplay.Manager.UI
{
    /// <summary>
    /// Manages the user interface, including panels, buttons, and timers during the game.
    /// </summary>
    public class UiManager : MonoBehaviour, IUIManager
    {
        [SerializeField] GameObject menuPanel;
        [SerializeField] GameObject GameplayPanel;
        [SerializeField] UiResultScreen ResultPanel;
        [SerializeField] Button gameStartButton;
        [SerializeField] Button startNextRoundButton;
        [SerializeField] TextMeshProUGUI roundNumberText;
        [SerializeField] TextMeshProUGUI TotalMonstersText;
        [SerializeField] GameStartCounter gameStartCounter;
        [SerializeField] LoadingScreen loadingScreen;
        [SerializeField] UiTimer uiTimer;

        /// <summary>
        /// Event invoked when the game start button is pressed.
        /// </summary>
        public Action OnGameStartButton { get; set; }

        /// <summary>
        /// Event invoked when the start next round button is pressed.
        /// </summary>
        public Action OnStartNextRoundButton { get; set; }

        private void Start()
        {
            gameStartButton.onClick.AddListener(GameStartButton);
            startNextRoundButton.onClick.AddListener(StartNextRoundButton);

            menuPanel.SetActive(true);
            GameplayPanel.SetActive(false);
            ResultPanel.gameObject.SetActive(false);
            loadingScreen.gameObject.SetActive(false);
        }

        /// <summary>
        /// Starts the game by displaying the gameplay panel and starting necessary UI elements.
        /// </summary>
        public async Task StartGame(int roundNumber, int totalMonsters)
        {
            GameplayPanel.SetActive(true);
            menuPanel.SetActive(false);
            ResultPanel.gameObject.SetActive(false);
            roundNumberText.text = "Round: " + roundNumber.ToString();
            TotalMonstersText.text = "Total Monsters\n" + totalMonsters;
            await gameStartCounter.StartCounter(3);
            _ = uiTimer.StartTimer();
        }

        /// <summary>
        /// Initiates the loading screen.
        /// </summary>
        public void StartLoading()
        {
            loadingScreen.StartLoading();
        }

        /// <summary>
        /// Stops the loading screen.
        /// </summary>
        public void StopLoading()
        {
            loadingScreen.StopLoading();
        }


        /// <summary>
        /// Stops the timer and retrieves the elapsed time.
        /// </summary>
        public void StopTimer(out float timeElapsed)
        {
            uiTimer.StopTimer(out timeElapsed);
        }

        /// <summary>
        /// Shows the result screen with player rankings and total elapsed time.
        /// </summary>
        public async Task ShowResult(string[] PlayersRanking, float totalTime)
        {
            GameplayPanel.SetActive(false);
            menuPanel.SetActive(false);
            ResultPanel.gameObject.SetActive(true);
            startNextRoundButton.gameObject.SetActive(false);
            await ResultPanel.ShowResult(PlayersRanking, totalTime);
            startNextRoundButton.gameObject.SetActive(true);
        }

        /// <summary>
        /// Invoked when the game start button is pressed.
        /// </summary>
        private void GameStartButton()
        {
            OnGameStartButton?.Invoke();
        }

        /// <summary>
        /// Invoked when the start next round button is pressed.
        /// </summary>
        private void StartNextRoundButton()
        {
            OnStartNextRoundButton?.Invoke();
        }
    }
}