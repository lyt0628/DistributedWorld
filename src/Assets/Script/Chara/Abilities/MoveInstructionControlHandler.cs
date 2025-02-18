

using GameLib.DI;

using QS.Chara;
using QS.Common.ComputingService;
using QS.Motor;
using QS.GameLib.Pattern.Pipeline;
using QS.GameLib.Rx.Relay;
using UnityEngine;

namespace QS.Executor.Domain.Handler
{

    class MoveInstructionControlHandler : InBoundPipelineHandlerAdapter
    {
        public class Msg
        {
            public float horizontal;
            public float vertical;
            public bool dash;
            public bool jump;
            public Vector3 baseRight;
            public Vector3 baseforword;
            public Vector3 baseUp;

        }


        [Injected]
        readonly DataSource<CharaControl.Input, CharaControl.State> controlledPointDataSource;

        [Injected]
        readonly CharaControl controlPointService;

        readonly CharaControl.Input snapshot;
        readonly Transform transform;
        readonly IMotion motion;
        CharaControl.Result translation;

        public MoveInstructionControlHandler(Transform transform)
        {
            CharaGlobal.Instance.DI.Inject(this);
            this.transform = transform;

            //snapshot = controlledPointDataSource.Create();
            snapshot = new CharaControl.Input();
            var dataRelay = Relay<CharaControl.Input>.Tick(() =>snapshot, out motion);
            var uuid = controlledPointDataSource.Add(dataRelay);
            var tRelay =  controlPointService.Get(uuid);
            tRelay.Subscrib((t) => translation = t);

        }
        Quaternion ComputeRotation(float hor, float vert, Vector3 baseRight,
          Vector3 baseForword, Quaternion quaternion)
        {
            Quaternion rotation = quaternion;
            var face = hor * baseRight + vert * baseForword;
            if (face.magnitude == 0f) return rotation;
            face.y = 0;
            face = face.normalized;

            rotation = Quaternion.LookRotation(face);

            return Quaternion.Slerp(quaternion,
                rotation,
                5f * Time.deltaTime);
        }
        public override void Read(IPipelineHandlerContext context, object msg)
        {
            Msg m = (Msg)msg;
            snapshot.position = transform.position;
            snapshot.horizontal = m.horizontal;
            snapshot.vertical = m.vertical;
            snapshot.jump = m.jump;
            snapshot.Rotation = transform.rotation;
            snapshot.baseRight = m.baseRight;
            snapshot.baseForword = m.baseforword;
            snapshot.baseUp = m.baseUp;
            snapshot.dash = m.dash;
            motion.Set();
               
            transform.SetPositionAndRotation(transform.position + translation.disp, 
                    ComputeRotation(snapshot.horizontal, snapshot.vertical,
                                    snapshot.baseRight, snapshot.baseForword, snapshot.Rotation));
            msg = new MoveInstructionAnimHandler.Msg()
            {
                speed = translation.speed,
                jump = m.jump,
                jumping = translation.inAir
            };
            context.Write(msg);
        }
    }
}
