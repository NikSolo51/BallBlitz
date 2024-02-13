using CodeBase.Infrastructure.AssetManagement;
using CodeBase.Infrastructure.Factory;
using CodeBase.Infrastructure.Services;
using CodeBase.Services.Input;
using CodeBase.Services.Randomaizer;
using CodeBase.Services.StaticData;
using Mirror;
using UnityEngine;

namespace CodeBase.Infrastructure.States
{
    public class BootstrapState : IState
    {
       
        private readonly GameStateMachine _stateMachine;
        private SceneLoader _sceneLoader;
        private AllServices _services;

        public BootstrapState(GameStateMachine stateMachine, SceneLoader sceneLoader, AllServices services)
        {
            _stateMachine = stateMachine;
            _sceneLoader = sceneLoader;
            _services = services;

            RegisterServices();
        }

        public void Enter()
        {
            _stateMachine.Enter<LoadLevelState,string>(Constants.Initial);
        }

        private void RegisterServices()
        {
            _services.RegisterSingle<IInputService>(SetupMovementInputService());
            _services.RegisterSingle<IGameStateMachine>(_stateMachine);
            
            RegisterAssetProvider();
            RegisterStaticData();
            
            _services.RegisterSingle<IRandomService>(new RandomService());

            GameFactory gameFactory = new GameFactory(
                _services.Single<IAssetProvider>(), _services.Single<IStaticDataService>());
            _services.RegisterSingle<IGameFactory>(gameFactory);
        }

        private void RegisterAssetProvider()
        {
            AssetProvider assetProvider = new AssetProvider();
            _services.RegisterSingle<IAssetProvider>(assetProvider);
        }

        private void RegisterStaticData()
        {
            IStaticDataService StaticData = new StaticDataService();
            StaticData.Initialize();
            _services.RegisterSingle<IStaticDataService>(StaticData);
        }

        public void Exit()
        {
        }

        private static IInputService SetupMovementInputService()
        {
           return new StandaloneInputService();
        }
    }
}