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
        IEnviroment enviroment;

        [Inject]
        private void Constructor(IUIManager uiManager, IRoundManager roundManager, IEnviroment enviroment)
        {
            this.uiManager = uiManager;
            this.roundManager = roundManager;
            this.enviroment = enviroment;
        }

        void Start()
        {
            uiManager.OnGameStartButton += OnGameStartButton;
            uiManager.OnStartNextRoundButton += OnStartNextRoundButton;
            roundManager.OnRoundFinished += AllMonsterReached;
        }

        private void OnGameStartButton()
        {
            _ = OnGameStart();
        }

        private void OnStartNextRoundButton()
        {
            _ = OnGameStart();
        }

        private async Task OnGameStart()
        {
            roundManager.InitializeRound(out int totalMonsters, (totalMonsters) => enviroment.SetEnviroment(totalMonsters));
            await uiManager.StartGame(roundManager.RoundNo, totalMonsters);
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
            uiManager.ShowResult(monsters.Select(monster => monster.MonsterName).ToArray(), timeElapsed.ToString());

            await Task.Delay(1);
            roundManager.RoundComplete();
            roundManager.DespawnRound();

        }
    }
}