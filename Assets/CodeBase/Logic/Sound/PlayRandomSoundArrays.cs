using System;
using UnityEngine;
using Random = UnityEngine.Random;

namespace CodeBase.Logic.Sound
{
    public class PlayRandomSoundArrays : MonoBehaviour
    {
        [SerializeField] private SoundData[] _soundDatas;
        [SerializeField] private AudioSource _source;
        [SerializeField] private bool _playWithRandomPitch;
        [SerializeField] private float _minRandomPitch;
        [SerializeField] private float _maxRandomPitch;

        public void PlaySound(int i)
        {
            _source.clip = _soundDatas[i]._audioClips[Random.Range(0, _soundDatas[i]._audioClips.Length)];
           
            if (_playWithRandomPitch)
            {
                _source.pitch = Random.Range(_minRandomPitch, _maxRandomPitch);
                _source.Play();
            }
            else
            {
                _source.pitch = 1;
                _source.Play();
            }
        }
    }

    [Serializable]
    public class SoundData
    {
        public AudioClip[] _audioClips;
    }
}