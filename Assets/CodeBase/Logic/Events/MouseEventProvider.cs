using System;
using UnityEngine;

namespace CodeBase.Logic.Events
{
    public class MouseEventProvider : MonoBehaviour
    {
        public event Action OnMouseUpEvent;
        public event Action OnMouseDownEvent;

        private void OnMouseUp()
        {
            OnMouseUpEvent?.Invoke();
        }

        private void OnMouseDown()
        {
            OnMouseDownEvent?.Invoke();
        }
    }
}