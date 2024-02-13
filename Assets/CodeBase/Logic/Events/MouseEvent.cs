using UnityEngine;
using UnityEngine.Events;

namespace CodeBase.Logic.Events
{
    public class MouseEvent : MonoBehaviour
    {
        [SerializeField] private UnityEvent onMouseUpUnityEvent;
        [SerializeField] private UnityEvent onMouseDownUnityEvent;

        private void OnMouseUp()
        {
            onMouseUpUnityEvent?.Invoke();
        }

        private void OnMouseDown()
        {
            onMouseDownUnityEvent?.Invoke();
        }
    }
}