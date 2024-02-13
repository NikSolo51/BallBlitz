using UnityEngine;
using UnityEngine.Events;

namespace CodeBase.Logic.Events
{
    public class OnEnableEvent : MonoBehaviour
    {
        public UnityEvent OnEnabled;

        private void OnEnable()
        {
            OnEnabled?.Invoke();
        }
    }
}