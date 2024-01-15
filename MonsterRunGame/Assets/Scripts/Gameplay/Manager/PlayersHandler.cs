using System.Collections;
using System.Collections.Generic;
using Gameplay.Interface;
using UnityEngine;

namespace Gameplay.Manager
{
    /// <summary>
    /// Handles player-related functionality and updates during gameplay.
    /// </summary>
    public class PlayersHandler : MonoBehaviour, IPlayersHandler
    {
        /// <summary>
        /// Array to store references to monster objects.
        /// </summary>
        IMonster[] monsters;

        /// <summary>
        /// Gets or sets a value indicating whether the game has started.
        /// </summary>
        public bool IsGameStarted { get; set; }

        /// <summary>
        /// Assigns an array of monsters to the players handler.
        /// </summary>
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
