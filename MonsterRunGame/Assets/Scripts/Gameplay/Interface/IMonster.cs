using System;
using System.Collections;
using System.Collections.Generic;
using ObjectPool.Interface;
using UnityEngine;

namespace Gameplay.Interface
{
    public interface IMonster : IPoolableObject
    {
        string MonsterName { get; set; }
        void Initialize(ref Action CanMove, string MonsterName);
        Action<IMonster> OnFinished { get; set; }
        void UpdateCall();
    }

}