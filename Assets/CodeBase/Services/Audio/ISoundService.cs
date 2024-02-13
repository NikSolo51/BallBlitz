
using CodeBase.Infrastructure.Services;

namespace CodeBase.Services.Audio
{
    public interface ISoundService : IService
    {
        void PlaySound (string soundName);
        void StopSound (string soundName);
        void StopAllSounds ();
        void SetVolume (float volume);
        void SetPitch (float pitch);
        void MuteSound (string soundName);
        void PauseSound(string soundName);
        void UnmuteSound (string soundName);
        float GetSoundLength(string soundName);
        void SetVolumeConcreteSound(string soundName, float volume);
        float GetSoundVolume(string soundName);
    }
}