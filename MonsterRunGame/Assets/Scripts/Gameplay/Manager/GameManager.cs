using System.Collections;
using System.Linq;
using System.Collections.Generic;
using System.Threading.Tasks;
using Gameplay.Manager.UI;
using ObjectPool;
using Gameplay.Interface;
using UnityEngine;
using Zenject;
using System;

namespace Gameplay.Manager
{
    /// <summary>
    /// Manages the game flow.
    /// </summary>
    public class GameManager : MonoBehaviour
    {
        /// <summary>
        /// Reference to the UI manager
        /// </summary>
        IUIManager uiManager;

        /// <summary>
        ///Reference to the round manager
        /// </summary>
        IRoundManager roundManager;

        /// <summary>
        ///Constructor for dependency injection
        /// </summary>
        [Inject]
        private void Constructor(IUIManager uiManager, IRoundManager roundManager)
        {
            this.uiManager = uiManager;
            this.roundManager = roundManager;
        }

        void Start()
        {
            uiManager.OnGameStartButton += OnGameStartButton;
            uiManager.OnStartNextRoundButton += OnStartNextRoundButton;
            roundManager.OnRoundFinished += AllMonsterReached;
            roundManager.OnRoundInitialized += OnRoundInitalized;
        }

        /// <summary>
        /// calls OnGameStart when user press Game Start button from menu
        /// </summary>
        private void OnGameStartButton()
        {
            OnGameStart();
        }

        /// <summary>
        /// calls OnGameStart when user press Next round button from Result Screen
        /// </summary>
        private void OnStartNextRoundButton()
        {
            OnGameStart();
        }

        /// <summary>
        /// Start the game by initializing the round
        /// </summary>
        private void OnGameStart()
        {
            uiManager.StartLoading();
            roundManager.InitializeRound();
        }


        private void OnRoundInitalized()
        {
            _ = StartGame();
        }

        /// <summary>
        /// Starts the game by stopping the loading, starting the game UI, and initiating the round.
        /// </summary>
        private async Task StartGame()
        {
            uiManager.StopLoading();
            await uiManager.StartGame(roundManager.RoundNo, roundManager.TotalMonsters);
            roundManager.StartRound();
        }

        /// <summary>
        ///
        /// </summary>
        private void AllMonsterReached(string[] monsters)
        {
            _ = RoundEnd(monsters);
        }

        /// <summary>
        /// Ends the current round by stopping the timer, showing the result, and completing the round.
        /// </summary>
        private async Task RoundEnd(string[] monsters)
        {
            await Task.Delay(TimeSpan.FromSeconds(1));
            uiManager.StopTimer(out float timeElapsed);
            await uiManager.ShowResult(monsters, timeElapsed);
            roundManager.RoundComplete();
            roundManager.DespawnRound();
        }
    }
}