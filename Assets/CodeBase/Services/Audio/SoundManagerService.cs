using CodeBase.SoundManager;
using UnityEngine;

namespace CodeBase.Services.Audio
{
    public class SoundManagerService : SoundManagerAbstract
    {
        void Start()
        {
            if (Application.isPlaying)
            {
                for (int i = 0; i < sounds.Length; i++)
                {
                    GameObject soundObject = new GameObject("Sound_" + i + "_" + sounds[i].name);
                    AudioSource audioSource = soundObject.AddComponent<AudioSource>();
                    sounds[i].SetSource(audioSource);
                    if (sounds[i].playOnAwake)
                        sounds[i].Play();
                    soundObject.transform.SetParent(this.transform);
                }
            }
        }

        public void ClipsToSounds()
        {
            int clipsLength = clips.Length;
            sounds = new Sound[clipsLength];
            int i = 0;
            foreach (AudioClip clip in clips)
            {
                sounds[i] = new Sound();
                sounds[i].name = clip.name;
                sounds[i].clip = clip;
                sounds[i].loop = false;
                sounds[i].randomPitch = 0.0f;
                sounds[i].randomVolume = 0.0f;
                sounds[i].masterPitch = 1.0f;
                sounds[i].masterVolume = 1.0f;
                sounds[i].pitch = 1.0f;
                sounds[i].volume = 1.0f;
                i++;
            }

            clips = new AudioClip[0];
        }

        public void AddClipsToSounds()
        {
            int clipsLength = clips.Length;
            int soundsLength = sounds.Length;
            var tempSounds = sounds;
            sounds = new Sound[soundsLength + clipsLength];

            int i = 0;

            foreach (var sound in tempSounds)
            {
                sounds[i] = tempSounds[i];
                i++;
            }

            foreach (AudioClip clip in clips)
            {
                sounds[i] = new Sound();
                sounds[i].name = clip.name;
                sounds[i].clip = clip;
                sounds[i].loop = false;
                sounds[i].randomPitch = 0.0f;
                sounds[i].randomVolume = 0.0f;
                sounds[i].masterPitch = 1.0f;
                sounds[i].masterVolume = 1.0f;
                sounds[i].pitch = 1.0f;
                sounds[i].volume = 1.0f;
                i++;
            }

            clips = new AudioClip[0];
        }

        public void DropEmptyNamed()
        {
            int soundsToStayCount = 0;
            int j = 0;
            for (int i = 0; i < sounds.Length; i++)
            {
                if (sounds[i].name != "")
                {
                    soundsToStayCount++;
                }
            }

            Sound[] soundsToStay = new Sound[soundsToStayCount];

            for (int i = 0; i < sounds.Length; i++)
            {
                if (sounds[i].name != "")
                {
                    soundsToStay[j] = sounds[i];
                    j++;
                }
            }

            sounds = new Sound[soundsToStayCount];

            for (int i = 0; i < sounds.Length; i++)
            {
                sounds[i] = soundsToStay[i];
            }
        }


        public override void PlaySound(string soundName)
        {
            for (int i = 0; i < sounds.Length; i++)
            {
                if (sounds[i].name == soundName)
                {
                    sounds[i].Play();
                    return;
                }
            }

            Debug.LogWarning("Sound with name " + soundName + " not found!");
        }

        public override void StopSound(string soundName)
        {
            for (int i = 0; i < sounds.Length; i++)
            {
                if (sounds[i].name == soundName)
                {
                    sounds[i].Stop();
                    return;
                }
            }

            Debug.LogWarning("Sound with name " + soundName + " not found!");
        }

        public override void StopAllSounds()
        {
            for (int i = 0; i < sounds.Length; i++)
            {
                sounds[i].Stop();
            }
        }

        public override void SetVolume(float volume)
        {
            for (int i = 0; i < sounds.Length; i++)
            {
                sounds[i].masterVolume = volume;
            }
        }
        
        public override void SetVolumeConcreteSound(string soundName, float volume)
        {
            for (int i = 0; i < sounds.Length; i++)
            {
               
                if (sounds[i].name == soundName)
                {
                    sounds[i].volume = volume;
                    sounds[i].source.volume = sounds[i].volume;
                }
            }
        }

        public override void SetPitch(float pitch)
        {
            for (int i = 0; i < sounds.Length; i++)
            {
                if (sounds[i].name != "button")
                {
                    sounds[i].masterPitch = pitch;
                }
            }
        }

        public override void MuteSound(string soundName)
        {
            for (int i = 0; i < sounds.Length; i++)
            {
                if (sounds[i].name == soundName)
                {
                    sounds[i].Mute();
                    return;
                }
            }
        }

        public override void PauseSound(string soundName)
        {
            for (int i = 0; i < sounds.Length; i++)
            {
                if (sounds[i].name == soundName)
                {
                    sounds[i].Pause();
                    return;
                }
            }
        }

        public override void UnmuteSound(string soundName)
        {
            for (int i = 0; i < sounds.Length; i++)
            {
                if (sounds[i].name == soundName)
                {
                    sounds[i].Unmute();
                    return;
                }
            }
        }

        public override float GetSoundLength(string soundName)
        {
            for (int i = 0; i < sounds.Length; i++)
            {
                if (sounds[i].name == soundName)
                {
                    return sounds[i].GetSoundLenght();
                }
            }

            return 0;
        }
        
        public override float GetSoundVolume(string soundName)
        {
            for (int i = 0; i < sounds.Length; i++)
            {
                if (sounds[i].name == soundName)
                {
                    return sounds[i].volume;
                }
            }

            return -1;
        }

        public float GetSoundCurPosition(string soundName)
        {
            for (int i = 0; i < sounds.Length; i++)
            {
                if (sounds[i].name == soundName)
                {
                    return sounds[i].GetSoundCurPosition();
                }
            }

            return 0;
        }
    }
}