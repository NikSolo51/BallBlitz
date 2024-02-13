using UnityEngine;

namespace CodeBase.Services.Input
{
    public abstract class InputService : IInputService
    {
        protected const string Horizontal = "Horizontal";
        protected const string Vertical = "Vertical";
        protected const string MouseY = "Mouse Y";
        protected const string MouseX = "Mouse X";

        public abstract Vector2 Axis { get; }
        public abstract Vector2 ViewAxis { get; }
        public abstract float ScrollAxis { get; }

        public bool IsClickButtonUp()
        {
            return UnityEngine.Input.GetMouseButtonUp(0);
        }

        public abstract bool IsClickButtonDown();


        public abstract bool IsClickButtonPress();

        protected static Vector2 UnityAxis()
        {
            Vector2 axis = new Vector2(UnityEngine.Input.GetAxis(Horizontal), UnityEngine.Input.GetAxis(Vertical));
            return axis;
        }

        protected static Vector2 MouseAxis()
        {
            Vector2 axis = new Vector2(UnityEngine.Input.GetAxis(MouseX), UnityEngine.Input.GetAxis(MouseY));
            return axis;
        }
    }
}