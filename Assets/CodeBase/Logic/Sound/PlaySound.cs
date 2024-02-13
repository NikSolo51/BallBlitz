using UnityEngine;

namespace CodeBase.Logic.Sound
{
    public class PlaySound : MonoBehaviour
    {
        [SerializeField] private AudioSource _source;
        [SerializeField] private AudioClip[] _audioClips;


        public void PlayAudioByIndex(int index)
        {
            _source.clip = _audioClips[index];
            _source.Play();
        }
    }
}