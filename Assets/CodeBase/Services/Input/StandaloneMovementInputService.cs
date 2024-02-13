using UnityEngine;

namespace CodeBase.Services.Input
{
    public class StandaloneInputService : InputService
    {
        public override Vector2 Axis => UnityAxis();

        public override Vector2 ViewAxis
        {
            get => MouseAxis();
        }

        public override bool IsClickButtonDown()
        {
            return UnityEngine.Input.GetMouseButtonDown(0);
        }

        public override bool IsClickButtonPress()
        {
            return UnityEngine.Input.GetMouseButton(0);
        }

        public override float ScrollAxis
        {
            get
            {
                if (UnityEngine.Input.GetAxis("Mouse ScrollWheel") > 0f) // forward
                {
                    return 1;
                }
                else if (UnityEngine.Input.GetAxis("Mouse ScrollWheel") < 0f) // backwards
                {
                    return -1;
                }

                return 0;
            }
        }
    }
}