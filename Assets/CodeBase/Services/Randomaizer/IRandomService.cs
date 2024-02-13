using CodeBase.Infrastructure.Services;

namespace CodeBase.Services.Randomaizer
{
    public interface IRandomService : IService
    {
        int Next(int minValue, int maxValue);
    }
}