using UnityEngine;

namespace CodeBase.Services.Randomaizer
{
    public class RandomService : IRandomService
    {
        public int Next(int min, int max) =>
            Random.Range(min, max);
    }
}