using CodeBase.SoundManager;
using UnityEngine;

namespace CodeBase.Services.StaticData
{
    [CreateAssetMenu(fileName = "SoundManagerData", menuName = "StaticData/SoundManager", order = 0)]
    public class SoundManagerStaticData : ScriptableObject
    {
        public int id; 
        public SoundManagerType SoundManagerType;
        public GameObject SoundManager;
    }
}