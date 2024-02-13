using CodeBase.Infrastructure.Services;
using UnityEngine;

namespace CodeBase.Services.Camera
{
    public interface ICameraRaycast : IService
    {
        public void EnableRayCast();
        public void DisableRayCast();
        public Vector3 GetPoint();
        public Collider GetCollider();
        Vector3 GetLastHitPoint();
    }
}