using System.Collections.Generic;
using System.Linq;
using CodeBase.SoundManager;
using UnityEngine;

namespace CodeBase.Services.StaticData
{
    public class StaticDataService : IStaticDataService
    {
        private Dictionary<SoundManagerType, SoundManagerStaticData> _soundManagers;

        public void Initialize()
        {
            _soundManagers = Resources.LoadAll<SoundManagerStaticData>("StaticData/SoundManagers")
                .ToDictionary(x => x.SoundManagerType, x => x);
        }

        public SoundManagerStaticData ForSoundManager(SoundManagerType soundManagerType)
        {
            return _soundManagers.TryGetValue(soundManagerType, out SoundManagerStaticData staticData)
                ? staticData
                : null;
        }
    }
}