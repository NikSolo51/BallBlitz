using System.Collections;
using CodeBase.Infrastructure.Services;
using Mirror;
using UnityEngine;

    [AddComponentMenu("")]
    public class MatchManager : NetworkManager, IMatchService
    {
        [Header("Match GUI")]
        public GameObject canvas;
        private ILobbyServiceProvider lobbyServiceProvider;
        public static new MatchManager singleton { get; private set; }

        public void Construct(ILobbyServiceProvider _lobbyServiceProvider, GameObject lobbyControllerCanvas)
        {
            lobbyServiceProvider = _lobbyServiceProvider;
            canvas = lobbyControllerCanvas;
            lobbyServiceProvider.InitializeData();
        }
        
        public override void Awake()
        {
            base.Awake();
            singleton = this;
        }

        #region Server System Callbacks

        public override void OnServerReady(NetworkConnectionToClient conn)
        {
            base.OnServerReady(conn);
            lobbyServiceProvider.OnServerReady(conn);
        }

        public override void OnServerDisconnect(NetworkConnectionToClient conn)
        {
            StartCoroutine(DoServerDisconnect(conn));
        }

        IEnumerator DoServerDisconnect(NetworkConnectionToClient conn)
        {
            yield return lobbyServiceProvider.OnServerDisconnect(conn);
            base.OnServerDisconnect(conn);
        }

        #endregion

        #region Client System Callbacks

       
        public override void OnClientConnect()
        {
            base.OnClientConnect();
            lobbyServiceProvider.OnClientConnect();
        }

        public override void OnClientDisconnect()
        {
            lobbyServiceProvider.OnClientDisconnect();
            base.OnClientDisconnect();
        }

        #endregion

        #region Start & Stop Callbacks

     
        public override void OnStartServer()
        {
            if (mode == NetworkManagerMode.ServerOnly)
                canvas.SetActive(true);

            lobbyServiceProvider.OnStartServer();
        }

      
        public override void OnStartClient()
        {
            canvas.SetActive(true);
            lobbyServiceProvider.OnStartClient();
        }

        public override void OnStopServer()
        {
            lobbyServiceProvider.OnStopServer();
            canvas.SetActive(false);
        }

        public override void OnStopClient()
        {
            lobbyServiceProvider.OnStopClient();
        }

        #endregion

        public void ServiceSetActive(bool newState)
        {
            gameObject.SetActive(newState);
        }
    }

public interface IMatchService : IService
{
    public void ServiceSetActive(bool newState);
}
