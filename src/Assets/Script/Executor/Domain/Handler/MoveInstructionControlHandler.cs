

using GameLib.DI;
using QS.Api.Control.Domain;
using QS.Api.Control.Service;
using QS.Api.Control.Service.DTO;
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
            public bool jump;
            public Vector3 baseRight;
            public Vector3 baseforword;
            public Vector3 baseUp;
        }


        [Injected]
        readonly IControlledPointDataSource controlledPointDataSource;
        [Injected]
        readonly IControlledPointService controlPointService;

        readonly IControlledPointData pointData;
        readonly Transform transform;
        readonly IMotion motion;
        ICharacterTranslationDTO translation;

        public MoveInstructionControlHandler(Transform transform)
        {
            ExecutorGlobal.Instance.DI.Inject(this);
            this.transform = transform;

            pointData = controlledPointDataSource.Create();
            var dataRelay = Relay<IControlledPointData>.Tick(() =>pointData, out motion);
            var uuid = controlledPointDataSource.New(dataRelay);
            var tRelay =  controlPointService.GetTranslation(uuid);
            tRelay.Subscrib((t) => translation = t);

        }

        public override void Read(IPipelineHandlerContext context, object msg)
        {
            Msg m = (Msg)msg;
            pointData.Position = transform.position;
            pointData.Horizontal = m.horizontal;
            pointData.Vertical = m.vertical;
            pointData.Jump = m.jump;
            pointData.Rotation = transform.rotation;
            pointData.BaseRight = m.baseRight;
            pointData.Baseforword = m.baseforword;
            pointData.BaseUp = m.baseUp;
            motion.Set();
            transform.rotation = translation.Rotation;
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
