namespace CodeBase.SoundManager
{
    public interface IAudioPlayable
    {
        void PlaySound(string clipName);
        void StopSound(string clipName);
        void MuteSound(string clipName);
        void UnMuteSound(string clipName);
        void PauseSound(string clipName);
        void UnPauseSound(string clipName);
        void MuteAllSounds();
        void UnMuteAllSounds();
        void StopAllSounds();
        void DestroyAudioSource(string clipName);
        void SetVolume(float modifier, string clipName = ""); // нельзя вызвать из UnityEvent. Если clipName пустой, то изменить громкость всем доступным аудио клипам
        void SetPitch(float modifier, string clipName = ""); // то же что и комментарий выше
        void SetMasterVolume(float modifier);
        void SetMasterPitch(float modifier);
        float GetSoundLength(string clipName);
        float GetSoundTime(string clipName);
    }
}