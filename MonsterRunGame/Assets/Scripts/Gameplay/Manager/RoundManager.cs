using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ObjectPool;
using UnityEngine;

namespace Gameplay.Manager
{
    public class RoundManager : MonoBehaviour
    {
        private int roundno = 1;
        public int RoundNo => roundno;

        [SerializeField] ObjectPooler objectPooler;

        public void StartRound()
        {

            InitializePlayer();
        }

        private void InitializePlayer()
        {

        }

    }
}