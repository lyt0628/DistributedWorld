

using QS.Common.ComputingService;
using QS.GameLib.Uitl.RayCast;
using QS.GameLib.Util.Raycast;
using UnityEngine;


namespace QS.Motor
{
    public static class MotorUtil
    {
        public static Vector3 ClampDisplacement(Vector3 position, Vector3 displacement, float error)
        {
            Vector3 result = displacement;
            var disp = displacement.magnitude;
            if (disp >= 0f)
            {
                var raycast = RaycastHelper
                    .Of(CastedObject
                    .Ray(position,displacement.normalized)
                    .IgnoreTrigger());
                if (raycast.IsCloserThanOrJust(Mathf.Max(disp, error)))
                {
                    result = (raycast.Distance - error) * displacement.normalized;
                }
            }

            return result;
        }
    }
}