using CodeBase.Infrastructure.Services;
using CodeBase.Services.Audio;
using UnityEngine;
using UnityEngine.UI;

namespace CodeBase.Logic.Sound
{
    public class SoundSliderController : MonoBehaviour
    {
        [SerializeField] private Slider _slider;
        [SerializeField] private string[] _soundNames;
        [SerializeField] private bool _synchronizeWithSoundManager = true;
        private ISoundService _soundService;

        private void Start()
        {
            _soundService = AllServices.Container.Single<ISoundService>();
            _slider.onValueChanged.AddListener (delegate {SetVolumeForSounds();});
            if (_synchronizeWithSoundManager)
            {
                _slider.value = _soundService.GetSoundVolume(_soundNames[0]);
            }
        }

        public void SetVolumeForSounds()
        {
            for (int i = 0; i < _soundNames.Length; i++)
            {
                _soundService.SetVolumeConcreteSound(_soundNames[i],_slider.value);
            }
        }
    }
}