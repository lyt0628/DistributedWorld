

using GameLib.DI;
using QS.Api.Setting;
using QS.Common.ComputingService;
using QS.GameLib.Rx.Relay;
using QS.GameLib.Uitl.RayCast;
using QS.GameLib.Util.Raycast;
using QS.Stereotype;
using UnityEngine;
using UnityEngine.Assertions;
using UnityEngine.TextCore.Text;

namespace QS.Motor
{
    public class MoveControl : AbstractComputingService<MoveControl.Input, MoveControl.Result, MoveControl.State>
    {
        readonly IGlobalPhysicSetting globalPhysic;
        readonly FreeFallControl grivityControl;
        readonly DataSource<FreeFallControl.Input, FreeFallControl.State> grivityControlDataSource;
        [Injected]
        public MoveControl(DataSource<Input, State> dataSource, FreeFallControl grivityControl, DataSource<FreeFallControl.Input, FreeFallControl.State> grivityControlDataSource, IGlobalPhysicSetting globalPhysic) : base(dataSource)
        {
            this.grivityControl = grivityControl;
            this.grivityControlDataSource = grivityControlDataSource;
            this.globalPhysic = globalPhysic;
        }

        protected override Result Compute(Input input, State state)
        {
            if (state.motion == null)
            {
                DecoratedByGrivityControl(input, state);
            }

            var result = new Result();
            state.motion.Set();

            var hVelocity = input.crossInput.x * input.right + input.crossInput.y * input.forward;
            hVelocity *= input.speedFactor;
            var velocityRay = RaycastHelper
             .Of(CastedObject
                 .Ray(input.position, hVelocity.normalized)
                 .IgnoreTrigger());

            //if (velocityRay.Hit && velocityRay.Distance < globalPhysic.ErrorTolerance && Vector3.Dot(velocityRay.Normal, input.up) > .5f)
            //{
            //    hVelocity = Vector3.zero;
            //}

            var hDisp = Time.deltaTime * hVelocity;
            var downRay = RaycastHelper
                    .Of(CastedObject
                        .Ray(input.position, -input.up)
                        .IgnoreTrigger());
            hDisp = Vector3.ProjectOnPlane(hDisp, downRay.Normal);

            var disp = state.grivityControlResult.disp + hDisp;
            var moveRay = RaycastHelper
                .Of(CastedObject
                    .Ray(input.position, disp.normalized)
                    .IgnoreTrigger());
            if (moveRay.IsCloserThanOrJust(Mathf.Max(disp.magnitude, globalPhysic.ErrorTolerance)))
            {
                disp = (moveRay.Distance - globalPhysic.HalfErrorTolerance) * disp.normalized;
            }

            result.disp = disp;
            result.speed = hVelocity.magnitude;
            result.face = Quaternion.LookRotation(hVelocity.normalized);
            return result;
            
        }

        protected override void DoReset(State state)
        {
      
        }

        private void DecoratedByGrivityControl(Input input, State state)
        {
            var dataRelay = Relay<FreeFallControl.Input>.Tick(() =>
            {
                state.grivityControlInput.grivityDir = -input.up;
                state.grivityControlInput.grivityValue = input.grivity;
                state.grivityControlInput.position = input.position;
                return state.grivityControlInput;
            }, out state.motion);
            var uuid = grivityControlDataSource.Add(dataRelay);
            grivityControl.Get(uuid).Subscrib((t) => state.grivityControlResult = t);
        }

        public class Input
        {
            public Vector2 crossInput;
            public Vector3 forward;
            public Vector3 up;
            public Vector3 right;
            public float speedFactor = 1.0f;

            public float grivity;
            public Vector3 position;
        }
        public class Result
        {
            public Vector3 disp;
            public float speed;
            public Quaternion face;
        }

        public class State
        {
            [Nullable] public IMotion motion;
            [Nullable] public FreeFallControl.Input grivityControlInput = new();
            [Nullable] public FreeFallControl.Result grivityControlResult;
        }
    }
}