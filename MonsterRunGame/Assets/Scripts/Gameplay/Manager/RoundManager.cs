using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Gameplay.Interface;
using Gameplay.Monsters;
using ObjectPool.Interface;
using UnityEngine;
using Extensions;
using Zenject;

namespace Gameplay.Manager
{
    /// <summary>
    /// Manages the rounds in the game, including monster spawning, round initialization, and completion.
    /// </summary>
    public class RoundManager : MonoBehaviour, IRoundManager
    {

        /// <summary>
        /// Property to get the current round number
        /// </summary>
        public int RoundNo => roundno;

        /// <summary>
        /// Property to get the total number of monsters in the round.
        /// </summary>
        public int TotalMonsters { get; private set; }

        /// <summary>
        /// Event invoked when the round is initialized.
        /// </summary>
        public Action OnRoundInitialized { get; set; }

        /// <summary>
        /// Event invoked when the round is completed.
        /// </summary>
        public Action<string[]> OnRoundFinished { get; set; }

        private IPlayersHandler playersHandler;

        private int roundno = 1;

        string[] monstersRank;

        private int moveRankingIndex;

        private Action MonsterCanMove;

        private Vector3 spawnPosition;

        private IMonster prefabMonster;

        private IObjectPooler objectPooler;

        private List<IMonster> monsters = new();

        /// <summary>
        /// Constructor for dependency injection
        /// </summary>
        [Inject]
        private void Constructor(IObjectPooler objectPooler, IMonster prefabMonster, IPlayersHandler playersHandler)
        {
            this.objectPooler = objectPooler;
            this.prefabMonster = prefabMonster;
            this.playersHandler = playersHandler;
        }

        /// <summary>
        /// Initializes the round by determining the number of monsters to spawn and spawning them.
        /// </summary>
        public async Task InitializeRound()
        {
            spawnPosition = MonsterPositon();
            moveRankingIndex = 0;
            int totalMonsters = roundno.GetFibonacciSequence();
            TotalMonsters = totalMonsters;
            monstersRank = new string[totalMonsters];
            await SpawnMonster(totalMonsters);
        }

        /// <summary>
        /// Invoked when all monsters have been spawned, assigns monsters to the players handler, and triggers the round initialized event.
        /// </summary>
        private void AllMonstersSpawned()
        {
            playersHandler.AssignMonsters(monsters.ToArray());
            OnRoundInitialized?.Invoke();
        }

        /// <summary>
        /// Completes the current round.
        /// Increments the round number
        /// </summary>
        public void RoundComplete()
        {
            roundno++;
        }

        /// <summary>
        /// Despawns the current round by stopping monster movement and resetting related variables.
        /// </summary>
        public void DespawnRound()
        {
            playersHandler.IsGameStarted = false;
            MonsterCanMove = null;
        }

        /// <summary>
        /// Starts the current round by allowing monsters to move.
        /// </summary>
        public void StartRound()
        {
            playersHandler.IsGameStarted = true;
            MonsterCanMove?.Invoke();
        }

        /// <summary>
        /// Spawns monsters based on the specified count.
        /// </summary>
        private async Task SpawnMonster(int count)
        {
            if (count <= 0 || !Application.isPlaying)
            {
                AllMonstersSpawned();
                return;
            }

            IMonster monster = objectPooler.Pool<Monster>(prefabMonster, transform);
            monster.Transform.position = spawnPosition;
            monsters.Add(monster);
            monster.OnFinished += OnMonsterFinishGame;
            monster.Initialize(ref MonsterCanMove, "Monster" + monsters.Count);
            await Task.Yield();
            _ = SpawnMonster(count - 1);
        }

        /// <summary>
        /// Calculates the position where monsters should be spawned.
        /// </summary>
        private Vector2 MonsterPositon()
        {
            Vector3 spawnPosition = Camera.main.ScreenToWorldPoint(new Vector3(0, Screen.height, 10));
            spawnPosition.x += 1;
            spawnPosition.y = 0;
            return spawnPosition;
        }

        /// <summary>
        /// Invoked when a monster Left the screen, updates rankings, and triggers the round finished event if all monsters have finished.
        /// </summary>
        private void OnMonsterFinishGame(IMonster monster)
        {
            monster.OnFinished -= OnMonsterFinishGame;
            monstersRank[moveRankingIndex] = monster.MonsterName;
            monsters.Remove(monster);
            objectPooler.Remove(monster);
            moveRankingIndex++;
            if (monsters.Count <= 0)
            {
                OnRoundFinished?.Invoke(monstersRank);
            }
        }
    }
}