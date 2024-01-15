using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Gameplay.Interface
{
    public interface IRoundManager
    {
        public int RoundNo { get; }
        Action<List<IMonster>> OnRoundFinished { get; set; }
        void InitializeRound(out int totalMonsters, Action<int> SetCamera);
        void RoundComplete();
        void DespawnRound();

        void StartRound();
    }
}
