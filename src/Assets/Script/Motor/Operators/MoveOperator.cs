

using QS.GameLib.Rx.Relay;
using QS.GameLib.Uitl.RayCast;
using QS.GameLib.Util.Raycast;
using UnityEngine;

namespace QS.Motor
{
    class MoveOperator : AbstractOperator<MoveInput, MotorResult>
    {

        protected override MotorResult OperateAsync(MoveInput t)
        {

            var result = new MotorResult();
            
            var hVelocity = t.horizontal * t.right + t.vertical * t.forward;
            hVelocity = hVelocity.normalized;
            hVelocity *= t.speedFactor;

            var disp = Time.deltaTime * hVelocity;
            var downRay = RaycastHelper
                    .Of(CastedObject
                        .Ray(t.position, -t.up)
                        .IgnoreTrigger());
            disp = Vector3.ProjectOnPlane(disp, downRay.Normal);

            result.position = t.position;
            result.displacement = disp;
            result.velocity = hVelocity; 
            return result;
        }
    }

}