using System.Collections;
using System.Collections.Generic;
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
            uiManager.OnGameStartButton += OnGameStart;
        }

        public void OnGameStart()
        {
            roundManager.StartRound();
            uiManager.StartGame(roundManager.RoundNo);
        }

        // private void OnGameComplete(List<il>)
        // {

        // }



    }
}