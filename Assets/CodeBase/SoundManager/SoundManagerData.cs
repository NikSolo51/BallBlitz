using System;
using UnityEngine;

namespace CodeBase.SoundManager
{
    [Serializable]
    public class SoundManagerData
    {
        public SoundManagerType _soundManagerType;
        public Sound[] _sounds;
        public AudioClip[] _clips;

        public SoundManagerData()
        {
        }

        public SoundManagerData(Sound[] sounds, AudioClip[] clips, SoundManagerType soundManagerType)
        {
            _soundManagerType = soundManagerType;
            _sounds = sounds;
            _clips = clips;
        }
    }
}