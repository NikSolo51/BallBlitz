using CodeBase.Services.Audio;
using UnityEngine;

namespace CodeBase.SoundManager
{
    public abstract class SoundManagerAbstract : MonoBehaviour,ISoundService
    {
        public Sound[] sounds;
        public AudioClip[] clips;
        public abstract void PlaySound(string soundName);
        public abstract void StopSound(string soundName);
        public abstract void StopAllSounds();
        public abstract void SetVolume(float volume);
        public abstract void SetVolumeConcreteSound(string soundName, float volume);
        public abstract void SetPitch(float pitch);
        public abstract void MuteSound(string soundName);
        public abstract void PauseSound(string soundName);
        public abstract void UnmuteSound(string soundName);
        public abstract float GetSoundLength(string soundName);
        public abstract float GetSoundVolume(string soundName);


    }
}