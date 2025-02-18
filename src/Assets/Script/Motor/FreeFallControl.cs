

using GameLib.DI;
using QS.Api.Setting;
using QS.Common.ComputingService;
using QS.GameLib.Uitl.RayCast;
using QS.GameLib.Util.Raycast;
using UnityEngine;
using UnityEngine.Assertions;

namespace QS.Motor
{
    public class FreeFallControl : AbstractComputingService<FreeFallControl.Input, FreeFallControl.Result, FreeFallControl.State>
    {
   
        readonly IGlobalPhysicSetting globalPhysic;

        [Injected]
        public FreeFallControl(DataSource<Input, State> dataSource, IGlobalPhysicSetting globalPhysic) : base(dataSource)
        {
            this.globalPhysic = globalPhysic;
        }


        public class Input 
        {
            public Vector3 grivityDir;
            public float grivityValue;
            public Vector3 position;
        }
        public class Result 
        {
            public Vector3 disp = Vector3.zero;
        };

        public class  State
        {
            public float speed = 0f;
        }


        protected override Result Compute(Input input, State state)
        {
            Assert.IsTrue(state.speed >= 0);

            var result = new Result();
            float dispScalar = 0f;
            var downRay = RaycastHelper
                .Of(CastedObject
                        .Ray(input.position, input.grivityDir)
                        .IgnoreTrigger());
            var isGrounded = downRay.IsCloserThan(globalPhysic.ErrorTolerance);
            if (!isGrounded)
            {
                state.speed += input.grivityValue;
                dispScalar = state.speed * Time.deltaTime;
            }
            else
            {
                state.speed = 0f;
            }

            if (downRay.IsCloserThanOrJust(Mathf.Max(dispScalar, globalPhysic.ErrorTolerance)))
            {
                dispScalar = downRay.Distance - globalPhysic.HalfErrorTolerance;
            }

            result.disp = dispScalar * input.grivityDir;
            return result;
        }

        protected override void DoReset(State state)
        {
            state.speed = 0f;
        }
    }
}