using System.Collections.Generic;
using System.Threading.Tasks;
using CodeBase.Infrastructure.AssetManagement;
using CodeBase.Services.Audio;
using CodeBase.Services.StaticData;
using CodeBase.SoundManager;
using Mirror;
using UnityEngine;

namespace CodeBase.Infrastructure.Factory
{
    public class GameFactory : IGameFactory
    {
        private readonly IAssetProvider _assets;
        private readonly IStaticDataService _staticData;


        public GameFactory(IAssetProvider assetses,
            IStaticDataService staticData)
        {
            _assets = assetses;
            _staticData = staticData;
        }
        public MatchManager CreateMatchManager()
        {
            GameObject networkManagerGO = InstantiateRegistered(AssetsAdress.MatchManager);
            MatchManager matchManager = networkManagerGO.GetComponent<MatchManager>();
            return matchManager;
        }

        public LobbyController CreateLobbyController()
        {
            GameObject lobbyControllerGO = InstantiateRegistered(AssetsAdress.LobbyController);
            LobbyController lobbyController = lobbyControllerGO.GetComponent<LobbyController>();
            return lobbyController;
        }

        public GameObject CreateCamera(Vector3 at)
        {
            GameObject cameraGameObject = InstantiateRegistered(AssetsAdress.Camera, at);
            return cameraGameObject;
        }

        public GameObject CreateHud() =>
            InstantiateRegistered(AssetsAdress.Hud);

        public ISoundService CreateSoundManager(SoundManagerData soundManagerData)
        {
            SoundManagerStaticData soundManagerManagerStaticData =
                _staticData.ForSoundManager(soundManagerData._soundManagerType);

            if (soundManagerData._soundManagerType == SoundManagerType.Nothing)
                Debug.Log("SoundManager Type is Nothing");

        

            GameObject soundManagerObject = InstantiateRegistered(soundManagerManagerStaticData.SoundManager);
            SoundManagerAbstract soundManagerAbstract = soundManagerObject.GetComponent<SoundManagerAbstract>();

            soundManagerAbstract.sounds = soundManagerData._sounds;
            soundManagerAbstract.clips = soundManagerData._clips;

            return soundManagerAbstract;
        }

        public GameObject CreateUpdateManager()
        {
            GameObject updateManager = InstantiateRegistered(AssetsAdress.UpdateManager);
            return updateManager;
        }


        private GameObject InstantiateRegistered(string prefabPath, Vector3 at)
        {
            GameObject gameObject = _assets.Instantiate(path: prefabPath, at: at);
            return gameObject;
        }

        private GameObject InstantiateRegistered(string prefabPath)
        {
            GameObject gameObject = _assets.Instantiate(path: prefabPath);
            return gameObject;
        }
        
        private GameObject InstantiateRegistered(GameObject prefab)
        {
            GameObject gameObject = _assets.Instantiate(prefab);
            return gameObject;
        }
        
        private GameObject InstantiateRegistered(GameObject prefab,Vector3 at)
        {
            GameObject gameObject = _assets.Instantiate(prefab,at);
            return gameObject;
        }
    }
}