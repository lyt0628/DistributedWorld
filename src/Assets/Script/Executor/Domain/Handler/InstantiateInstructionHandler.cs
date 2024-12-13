


using QS.Api.Executor.Domain;
using QS.Executor.Domain.Instruction;
using QS.GameLib.Pattern.Pipeline;
using QS.GameLib.Util;
using UnityEngine;

namespace QS.Executor.Domain.Handler
{
    class InstantiateInstructionHandler : AbstractHandler
    {
        public InstantiateInstructionHandler(IRelayExecutor executor) : base(executor)
        {
        }

        public override void Read(IPipelineHandlerContext context, object msg)
        {
            if(!ReflectionUtil.IsChildOf<IInstantiateInstruction>(msg))
            {
                context.Write(msg);
                return;
            }

            var i = (InstantiateInstruction)msg;
            var prefab = Resources.Load<GameObject>(i.Prefab);
            var go = GameObject.Instantiate(prefab, i.Parent);
            go.transform.localPosition = i.Position;
            go.transform.localRotation = i.Rotation;
        }

    }
}