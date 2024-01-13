using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using Gameplay.Manager.UI;
using ObjectPool;
using UnityEngine;

namespace Gameplay.Manager
{
    public class GameManager : MonoBehaviour
    {
        [SerializeField] ObjectPooler objectPooler;
        [SerializeField] UiManager uiManager;
        [SerializeField] RoundManager roundManager;

        void Start()
        {
            uiManager.OnGameStartButton += OnGameStartButton;
            roundManager.OnRoundFinished += OnRoundEnd;
        }

        private void OnGameStartButton()
        {
            _ = OnGameStart();
        }

        private async Task OnGameStart()
        {
            roundManager.InitializeRound();
            await uiManager.StartGame(roundManager.RoundNo);
            roundManager.StartRound();
        }

        private void OnRoundEnd(List<IMonster> monsters)
        {
            roundManager.DespawnRound();
        }


    }
}