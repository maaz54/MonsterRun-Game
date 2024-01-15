using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Gameplay.Interface
{
    public interface IPlayersHandler
    {
        bool IsGameStarted { get; set; }
        void AssignMonsters(IMonster[] monsters);
    }
}
