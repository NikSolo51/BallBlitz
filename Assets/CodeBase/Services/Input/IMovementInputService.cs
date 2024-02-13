using CodeBase.Infrastructure.Services;
using UnityEngine;

namespace CodeBase.Services.Input
{
    public interface IInputService : IService
    {
        Vector2 Axis { get; }
        Vector2 ViewAxis { get; }
        float ScrollAxis { get; }
        bool IsClickButtonUp();
        bool IsClickButtonDown();
        bool IsClickButtonPress();
    };
}