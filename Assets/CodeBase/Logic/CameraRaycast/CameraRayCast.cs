using CodeBase.Services.Camera;
using UnityEngine;
using UnityEngine.EventSystems;

namespace CodeBase.Logic.CameraRaycast
{
    public class CameraRayCast : MonoBehaviour, ICameraRaycast
    {
        public LayerMask _layermask;
        private Camera _camera;
        private Vector3 _lastHitPointPos;
        private bool _enableRaycast = true;
        private Transform _heroTransform;

        public Vector3 GetLastHitPoint()
        {
            return _lastHitPointPos;
        }

        public Collider GetCollider()
        {
            RaycastHit hit;
            Ray ray = _camera.ScreenPointToRay(Input.mousePosition);
            if (Physics.Raycast(ray, out hit, Mathf.Infinity, _layermask))
            {
                return hit.collider;
            }

            return null;
        }
        
        public Vector3 GetPoint()
        {
            if (!_enableRaycast)
                return _heroTransform.position;
            
            if (EventSystem.current.IsPointerOverGameObject()) 
                return _heroTransform.position;
            if (EventSystem.current.currentSelectedGameObject != null)
                return _heroTransform.position;
            
            RaycastHit hit;
            Ray ray = _camera.ScreenPointToRay(Input.mousePosition);
            if (Physics.Raycast(ray, out hit, Mathf.Infinity, _layermask))
            {
                _lastHitPointPos = hit.point;
            }

            return _lastHitPointPos;
        }

        public void Construct(Camera camera,Transform heroTransform)
        {
            _camera = camera;
            _heroTransform = heroTransform;
        }

        public void EnableRayCast()
        {
            _enableRaycast = true;
        }

        public void DisableRayCast()
        {
            _enableRaycast = false;
        }
    }
}