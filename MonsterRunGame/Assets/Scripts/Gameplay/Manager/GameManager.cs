using System.Collections;
using System.Linq;
using System.Collections.Generic;
using System.Threading.Tasks;
using Gameplay.Manager.UI;
using ObjectPool;
using Gameplay.Interface;
using UnityEngine;
using Zenject;

namespace Gameplay.Manager
{
    public class GameManager : MonoBehaviour
    {
        IUIManager uiManager;
        IRoundManager roundManager;

        [Inject]
        private void Constructor(IUIManager uiManager, IRoundManager roundManager, IEnviroment enviroment)
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

        private void OnGameStartButton()
        {
            OnGameStart();
        }

        private void OnStartNextRoundButton()
        {
            OnGameStart();
        }

        private void OnGameStart()
        {
            uiManager.StartLoading();
            roundManager.InitializeRound();
        }

        private void OnRoundInitalized()
        {
            _ = StartGame();
        }

        private async Task StartGame()
        {
            uiManager.StopLoading();
            await uiManager.StartGame(roundManager.RoundNo, roundManager.TotalMonsters);
            roundManager.StartRound();
        }

        private void AllMonsterReached(List<IMonster> monsters)
        {
            _ = RoundEnd(monsters);
        }

        private async Task RoundEnd(List<IMonster> monsters)
        {
            await Task.Delay(1);
            uiManager.StopTimer(out float timeElapsed);
            await uiManager.ShowResult(monsters.Select(monster => monster.MonsterName).ToArray(), timeElapsed);
            roundManager.RoundComplete();
            roundManager.DespawnRound();

        }
    }
}