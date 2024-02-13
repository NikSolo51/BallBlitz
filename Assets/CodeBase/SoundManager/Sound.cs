using System;
using UnityEngine;
using Random = UnityEngine.Random;

namespace CodeBase.SoundManager
{
    [Serializable]
    public class Sound {

        public string name;
        public AudioClip clip;

        public float masterVolume = 1.0f;
        public float masterPitch = 1.0f;

        [Range(0.0f, 1.0f)]
        public float volume = 1.0f;
        [Range(0.5f, 1.5f)]
        public float pitch = 1.0f;

        [Range(0.0f, 0.5f)]
        public float randomVolume = 0.1f;
        [Range(0.0f, 0.5f)]
        public float randomPitch = 0.1f;
        
        public bool playOnAwake = false;
        public bool loop = false;

        [HideInInspector]public AudioSource source;

        public void SetSource(AudioSource _source)
        {
            source = _source;
            source.clip = clip;
            source.loop = loop;
            source.playOnAwake = playOnAwake;
        }

        public void Play ()
        {
           
            if (source) {
                
                source.volume = volume * (1 + Random.Range (-randomVolume / 2.0f, randomVolume / 2.0f)) * masterVolume;
                source.pitch = pitch * (1 + Random.Range (-randomPitch / 2.0f, randomPitch / 2.0f)) * masterPitch;
                source.Play ();
            }
        }

        public void Pause()
        {
            if (source)
            {
                source.Pause();
            }
        }

        public void Stop ()
        {
            if (source)
            {
                source.Stop();
            }
        }

        public void Mute ()
        {
            if (source)
            {
                source.mute = true;
            }
        }

        public void Unmute ()
        {
            if (source)
            {
                source.mute = false;
            }
        }

        public float GetSoundLenght()
        {
            float tmp = new float();
            if (source)
            {
                tmp = source.clip.length;
            }
            return tmp;
        }
        public float GetSoundCurPosition()
        {
            float tmp = new float();
            if (source)
            {
                tmp = source.time;
            }
            return tmp;
        }
    }
}