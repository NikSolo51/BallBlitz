using System.Threading.Tasks;
using CodeBase.Infrastructure.Factory;
using CodeBase.Infrastructure.Services;
using CodeBase.Logic;
using CodeBase.Services.Audio;
using CodeBase.Services.Input;
using CodeBase.Services.StaticData;
using CodeBase.SoundManager;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace CodeBase.Infrastructure.States
{
    public class LoadLevelState : IPayLoadedState<string>
    {
        private readonly GameStateMachine _stateMachine;
        private readonly SceneLoader _sceneLoader;
        private readonly LoadingCurtain _curtain;
        private readonly IGameFactory _gameFactory;
        private IStaticDataService _staticData;

        public LoadLevelState(GameStateMachine stateMachine,
            SceneLoader sceneLoader,
            LoadingCurtain curtain,
            IGameFactory gameFactory,
            IStaticDataService staticData)
        {
            _stateMachine = stateMachine;
            _sceneLoader = sceneLoader;
            _curtain = curtain;
            _gameFactory = gameFactory;
            _staticData = staticData;
        }

        public void Enter(string sceneName)
        {
            _curtain.Show();
            _sceneLoader.Load(sceneName, OnLoaded);
        }

        public void Exit()
        {
            _curtain.Hide();
        }

        private void OnLoaded()
        {
             InitGameWorld();
            _stateMachine.Enter<GameLoopState>();
        }


        private void  InitGameWorld()
        {
            LobbyController lobbyController = _gameFactory.CreateLobbyController();
            lobbyController.Construct(_sceneLoader);
            AllServices.Container.RegisterSingle<ILobbyServiceProvider>(lobbyController);
            
            MatchManager matchManager = _gameFactory.CreateMatchManager();
            matchManager.Construct(lobbyController,lobbyController.canvas);
        }
    }
}