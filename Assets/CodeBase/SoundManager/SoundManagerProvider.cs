using CodeBase.Services.Audio;
using UnityEngine;

namespace CodeBase.SoundManager
{
    public class SoundManagerProvider : MonoBehaviour, ISoundService
    {
        private ISoundService _soundService;

        public void Construct(ISoundService soundService)
        {
            _soundService = soundService;
        }

        public void PlaySound(string soundName)
        {
            _soundService.PlaySound(soundName);
        }

        public void StopSound(string soundName)
        {
            _soundService.StopSound(soundName);
        }

        public void StopAllSounds()
        {
            _soundService.StopAllSounds();
        }

        public void SetVolume(float volume)
        {
            _soundService.SetVolume(volume);
        }

        public void SetPitch(float pitch)
        {
            _soundService.SetPitch(pitch);
        }

        public void MuteSound(string soundName)
        {
            _soundService.MuteSound(soundName);
        }

        public void PauseSound(string soundName)
        {
            _soundService.PauseSound(soundName);
        }

        public void UnmuteSound(string soundName)
        {
            _soundService.UnmuteSound(soundName);
        }

        public float GetSoundLength(string soundName)
        {
            return _soundService.GetSoundLength(soundName);
        }

        public void SetVolumeConcreteSound(string soundName, float volume)
        {
            _soundService.SetVolumeConcreteSound(soundName, volume);
        }

        public float GetSoundVolume(string soundName)
        {
            return _soundService.GetSoundVolume(soundName);
        }
    }
}