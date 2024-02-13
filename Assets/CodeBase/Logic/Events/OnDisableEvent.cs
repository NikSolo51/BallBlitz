using UnityEngine;
using UnityEngine.Events;

namespace CodeBase.Logic.Events
{
    class OnDisableEvent : MonoBehaviour
    {
        public UnityEvent OnDisabled;

        private void OnDisable()
        {
            OnDisabled?.Invoke();
        }
    }
}