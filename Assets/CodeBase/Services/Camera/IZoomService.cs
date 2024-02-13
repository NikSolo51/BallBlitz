using CodeBase.Infrastructure.Services;

namespace CodeBase.Services.Camera
{
    public interface IZoomService : IService
    {
        void Zoom(float zoomSpeed,bool inverseScroll,float zoomMinBound,float zoomMaxBound);
    }
}