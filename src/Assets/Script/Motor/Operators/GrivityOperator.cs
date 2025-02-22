

using GameLib.DI;
using QS.Api.Setting;
using QS.GameLib.Rx.Relay;
using QS.GameLib.Uitl.RayCast;
using QS.GameLib.Util.Raycast;
using UnityEngine;
using UnityEngine.Assertions;

namespace QS.Motor
{
    class GrivityOperator : AbstractOperator<GrivityInput, MotorResult>
    {
        [Injected]
        readonly IGlobalPhysicSetting physicSetting;

        float speed = 0f;
        Vector3 grivityDir;
        readonly float grivityValue;

        public GrivityOperator(Vector3 grivityDir, float grivityValue)
        {
            this.grivityDir = grivityDir;
            this.grivityValue = grivityValue;
        }

        protected override MotorResult OperateAsync(GrivityInput t)
        {
            var result = new MotorResult
            {
                position = t.Position
            };

            float dispScalar = 0f;
            var downRay = RaycastHelper
                .Of(CastedObject
                        .Ray(t.Position, grivityDir)
                        .IgnoreTrigger());
            var isGrounded = downRay.IsCloserThan(physicSetting.ErrorTolerance);
            if (!isGrounded)
            {
                speed += grivityValue;
                dispScalar = speed * Time.deltaTime;
            }
            else
            {
                speed = 0f;
            }

            result.displacement = dispScalar * grivityDir;
            return result;
        }

    }
}