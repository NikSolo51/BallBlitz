using UnityEngine;

namespace CodeBase.Logic.Sound
{
    public class PlayRandomSound : MonoBehaviour
    {
        [SerializeField] private AudioSource _source;
        [SerializeField] private AudioClip[] _audioClips;
        [SerializeField] private bool _playWithRandomPitch;
        [SerializeField] private float _minRandomPitch;
        [SerializeField] private float _maxRandomPitch;

        public void PlaySound()
        {
            _source.clip = _audioClips[Random.Range(0, _audioClips.Length)];
            if (_playWithRandomPitch)
            {
                _source.pitch = Random.Range(_minRandomPitch,_maxRandomPitch);
                _source.Play();
            }
            else
            {
                _source.pitch = 1;
                _source.Play();    
            }
        }

        public void PlayRandom(int i)
        {
            
        }
    }
}