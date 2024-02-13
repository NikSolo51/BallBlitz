using System.Threading.Tasks;
using CodeBase.Infrastructure.Services;
using CodeBase.Services.Audio;
using CodeBase.SoundManager;
using UnityEngine;

namespace CodeBase.Infrastructure.Factory
{
    public interface IGameFactory : IService
    {
        GameObject CreateHud();
        ISoundService CreateSoundManager(SoundManagerData soundManagerData);
        GameObject CreateUpdateManager();
        GameObject CreateCamera(Vector3 at);

        MatchManager CreateMatchManager();
        LobbyController CreateLobbyController();
    }
}