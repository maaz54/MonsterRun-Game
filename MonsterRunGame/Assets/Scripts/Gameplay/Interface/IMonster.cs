using System;
using System.Collections;
using System.Collections.Generic;
using ObjectPool.Interface;
using UnityEngine;

public interface IMonster : IPoolableObject
{
    void StartRound();
    Action<IMonster> OnFinished { get; set; }
}
