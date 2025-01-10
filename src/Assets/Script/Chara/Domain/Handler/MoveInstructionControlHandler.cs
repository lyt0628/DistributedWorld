

using GameLib.DI;
using QS.Api.Control.Domain;
using QS.Api.Control.Service;
using QS.Api.Control.Service.DTO;
using QS.Chara;
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
        readonly ICharaTranslationProxyDataSource controlledPointDataSource;
        [Injected]
        readonly ICharaTranslationControl controlPointService;

        readonly ICharaTranslationSnapshot snapshot;
        readonly Transform transform;
        readonly IMotion motion;
        ICharaTranslation translation;

        public MoveInstructionControlHandler(Transform transform)
        {
            CharaGlobal.Instance.DI.Inject(this);
            this.transform = transform;

            snapshot = controlledPointDataSource.Create();
            var dataRelay = Relay<ICharaTranslationSnapshot>.Tick(() =>snapshot, out motion);
            var uuid = controlledPointDataSource.New(dataRelay);
            var tRelay =  controlPointService.GetTranslation(uuid);
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
            snapshot.Position = transform.position;
            snapshot.Horizontal = m.horizontal;
            snapshot.Vertical = m.vertical;
            snapshot.Jump = m.jump;
            snapshot.Rotation = transform.rotation;
            snapshot.BaseRight = m.baseRight;
            snapshot.BaseForword = m.baseforword;
            snapshot.BaseUp = m.baseUp;
            snapshot.Dash = m.dash;
            motion.Set();
             transform.rotation = ComputeRotation(snapshot.Horizontal, snapshot.Vertical,
                         snapshot.BaseRight, snapshot.BaseForword, snapshot.Rotation);
  
            transform.position += translation.Displacement;

            msg = new MoveInstructionAnimHandler.Msg()
            {
                speed = translation.Speed,
                jump = translation.Jumping
            };
            context.Write(msg);
        }
    }
}
