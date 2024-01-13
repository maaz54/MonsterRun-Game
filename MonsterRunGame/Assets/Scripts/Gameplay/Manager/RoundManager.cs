using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Gameplay.Monsters;
using ObjectPool;
using UnityEngine;

namespace Gameplay.Manager
{
    public class RoundManager : MonoBehaviour
    {
        private int roundno = 6;
        public int RoundNo => roundno;
        [SerializeField] Monster prefabMonster;
        [SerializeField] ObjectPooler objectPooler;

        [SerializeField] List<IMonster> monsters = new();

        [SerializeField] List<IMonster> monstersRank = new();

        public Action<List<IMonster>> OnRoundFinished;

        public void InitializeRound()
        {
            SpawnMonster(GetNextFibonacci(RoundNo));
        }

        public void DespawnRound()
        {
            Debug.Log(monsters.Count);
            monstersRank.Clear();
            DespanwnMonster(monsters.Count);

            void DespanwnMonster(int Count)
            {
                if (Count > 0)
                {
                    objectPooler.Remove(monsters[Count - 1]);
                    Count--;
                    DespanwnMonster(Count);
                }
            }
        }

        public void StartRound()
        {
            monsters.ForEach(m => m.StartRound());
        }

        private void SpawnMonster(int count)
        {
            if (count <= 0)
                return;

            Vector3 spawnPosition = Camera.main.ScreenToWorldPoint(new Vector3(0, UnityEngine.Random.Range(0, Screen.height), 10));
            spawnPosition.y = Mathf.Clamp(spawnPosition.y, -4.5f, 4.5f);

            Monster monster = objectPooler.Pool<Monster>(prefabMonster, transform);

            SpawnMonster(count - 1);

            monsters.Add(monster);
            monster.OnFinished += OnMonsterFinishGame;
        }

        private void OnMonsterFinishGame(IMonster monster)
        {
            monster.OnFinished -= OnMonsterFinishGame;
            monstersRank.Add(monster);

            if (monstersRank.Count == monsters.Count)
            {
                OnRoundFinished?.Invoke(monstersRank);
            }
        }


        int GetNextFibonacci(int current)
        {
            // Function to calculate the next number in the Fibonacci sequence
            // Implement the logic based on your needs
            return current + 1;
        }

    }
}