using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Gameplay.Monsters;
using ObjectPool;
using Unity.VisualScripting;
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

        private Action MonsterCanMove;

        public void InitializeRound()
        {
            SpawnMonster(GetNextFibonacci(RoundNo));
        }

        public void RoundComplete()
        {
            roundno++;
        }

        public void DespawnRound()
        {
            monstersRank.Clear();
            DespanwnMonster();
            MonsterCanMove = null;

            void DespanwnMonster()
            {
                if (monsters.Count > 0)
                {
                    objectPooler.Remove(monsters.First());
                    monsters.Remove(monsters.First());
                    DespanwnMonster();
                }
            }
        }

        public void StartRound()
        {
            MonsterCanMove?.Invoke();
        }

        private void SpawnMonster(int count)
        {
            if (count <= 0)
                return;


            IMonster monster = objectPooler.Pool<Monster>(prefabMonster, transform);
            monster.Transform.position = MonsterPositon(monsters.Count > 0 ? monsters[monsters.Count - 1].Transform.position.y : 0);


            monsters.Add(monster);
            monster.OnFinished += OnMonsterFinishGame;
            monster.Initialize(ref MonsterCanMove, "Monster" + monsters.Count);

            SpawnMonster(count - 1);

        }

        private Vector2 MonsterPositon(float yStartPos)
        {
            Vector3 spawnPosition = Camera.main.ScreenToWorldPoint(new Vector3(0, UnityEngine.Random.Range(0, Screen.height), 10));
            spawnPosition.x += 1;
            spawnPosition.y = yStartPos;

            // Alternate the Y position
            if (monsters.Count % 2 == 0)
            {
                spawnPosition.y += monsters.Count;
            }
            else
            {
                spawnPosition.y -= monsters.Count;
            }

            return spawnPosition;

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