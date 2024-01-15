using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;

namespace Gameplay.Interface
{
    public interface IRoundManager
    {
        int RoundNo { get; }
        int TotalMonsters { get; }
        Action OnRoundInitialized { get; set; }
        Action<string[]> OnRoundFinished { get; set; }
        Task InitializeRound();
        void RoundComplete();
        void DespawnRound();

        void StartRound();
    }
}
