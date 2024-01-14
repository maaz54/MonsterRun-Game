using System.Collections;
using System.Linq;
using System.Collections.Generic;
using System.Threading.Tasks;
using Gameplay.Manager.UI;
using ObjectPool;
using UnityEngine;
using TMPro;
using Gameplay.Monsters;

namespace Gameplay.Manager
{
    public class GameManager : MonoBehaviour
    {
        [SerializeField] ObjectPooler objectPooler;
        [SerializeField] UiManager uiManager;
        [SerializeField] RoundManager roundManager;
        [SerializeField] EnviromentAdjustment cameraOffset;

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
            roundManager.InitializeRound(out int totalMonsters, (totalMonsters) => cameraOffset.SetEnviroment(totalMonsters));
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