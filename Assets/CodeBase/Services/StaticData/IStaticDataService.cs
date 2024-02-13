using System.Collections.Generic;
using CodeBase.Infrastructure.Services;
using CodeBase.SoundManager;

namespace CodeBase.Services.StaticData
{
    public interface IStaticDataService : IService
    {
        void Initialize();

        SoundManagerStaticData ForSoundManager(SoundManagerType soundManagerType);
    }
}