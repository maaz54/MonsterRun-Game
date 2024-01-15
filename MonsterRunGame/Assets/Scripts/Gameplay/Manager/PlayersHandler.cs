using System.Collections;
using System.Collections.Generic;
using Gameplay.Interface;
using UnityEngine;

namespace Gameplay.Manager
{
    public class PlayersHandler : MonoBehaviour, IPlayersHandler
    {
        IMonster[] monsters;

        public bool IsGameStarted { get; set; }

        public void AssignMonsters(IMonster[] monsters)
        {
            this.monsters = monsters;
        }

        private void Update()
        {
            if (IsGameStarted)
            {
                for (int i = 0; i < monsters.Length; ++i)
                {
                    monsters[i].UpdateCall();
                }
            }
        }

    }
}
