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
    public class RoundManager : MonoBehaviour, IRoundManager
    {
        IPlayersHandler playersHandler;
        [SerializeField] private int roundno = 1;
        public int RoundNo => roundno;
        public int TotalMonsters { get; private set; }
        public Action OnRoundInitialized { get; set; }
        IMonster prefabMonster;
        IObjectPooler objectPooler;
        List<IMonster> monsters = new();
        string[] monstersRank;
        int moveRankingIndex;
        public Action<string[]> OnRoundFinished { get; set; }
        private Action MonsterCanMove;
        Vector3 spawnPosition;


        [Inject]
        private void Constructor(IObjectPooler objectPooler, IMonster prefabMonster, IPlayersHandler playersHandler)
        {
            this.objectPooler = objectPooler;
            this.prefabMonster = prefabMonster;
            this.playersHandler = playersHandler;
        }

        public async Task InitializeRound()
        {
            spawnPosition = MonsterPositon();
            moveRankingIndex = 0;
            int totalMonsters = roundno.GetFibonacciSequence();
            TotalMonsters = totalMonsters;
            monstersRank = new string[totalMonsters];
            await SpawnMonster(totalMonsters);
        }

        private void AllMonstersSpawned()
        {
            playersHandler.AssignMonsters(monsters.ToArray());
            OnRoundInitialized?.Invoke();
        }


        public void RoundComplete()
        {
            roundno++;
        }

        public void DespawnRound()
        {
            playersHandler.IsGameStarted = false;
            MonsterCanMove = null;
        }

        public void StartRound()
        {
            playersHandler.IsGameStarted = true;
            MonsterCanMove?.Invoke();
        }

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

        private Vector2 MonsterPositon()
        {
            Vector3 spawnPosition = Camera.main.ScreenToWorldPoint(new Vector3(0, Screen.height, 10));
            spawnPosition.x += 1;
            spawnPosition.y = 0;
            return spawnPosition;
        }

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