using System;
using System.Threading.Tasks;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Gameplay.Interface
{
    public interface IUIManager
    {
        Action OnGameStartButton { get; set; }
        Action OnStartNextRoundButton { get; set; }
        Task StartGame(int roundNumber, int totalMonsters);
        void StopTimer(out float timeElapsed);
        Task ShowResult(string[] PlayersRanking, float totalTime);
        public void StartLoading();
        public void StopLoading();
    }
}
