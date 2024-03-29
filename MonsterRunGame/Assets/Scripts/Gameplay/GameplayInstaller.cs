using System.Collections;
using System.Collections.Generic;
using Gameplay.Interface;
using Gameplay.Manager;
using Gameplay.Manager.UI;
using Gameplay.Monsters;
using ObjectPool;
using ObjectPool.Interface;
using UnityEngine;
using Zenject;

namespace Gameplay.Installer
{
    /// <summary>
    /// Installer class for setting up Zenject bindings for gameplay-related dependencies.
    /// </summary>
    public class GameplayInstaller : MonoInstaller<GameplayInstaller>
    {
        [SerializeField] EnviromentAdjustment enviromentAdjustment;
        [SerializeField] RoundManager roundManager;
        [SerializeField] UiManager uiManager;
        [SerializeField] ObjectPooler objectPooler;
        [SerializeField] Monster monsterPrefab;
        [SerializeField] PlayersHandler playersHandler;

        /// <summary>
        /// Configures Zenject bindings for gameplay-related dependencies.
        /// </summary>
        public override void InstallBindings()
        {
            Container.Bind<IEnviroment>().FromInstance(enviromentAdjustment).AsSingle();
            Container.Bind<IRoundManager>().FromInstance(roundManager).AsSingle();
            Container.Bind<IUIManager>().FromInstance(uiManager).AsSingle();
            Container.Bind<IObjectPooler>().FromInstance(objectPooler).AsSingle();
            Container.Bind<IMonster>().FromInstance(monsterPrefab).AsSingle();
            Container.Bind<IPlayersHandler>().FromInstance(playersHandler).AsSingle();
        }
    }
}
