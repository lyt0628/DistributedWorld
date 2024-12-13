



using GameLib.DI;
using QS.Api.Control.Domain;
using QS.Api.Control.Service;
using QS.Api.Executor.Domain;
using QS.Api.Executor.Domain.Instruction;
using QS.Executor.Domain.Instruction;
using QS.GameLib.Pattern.Message;
using QS.GameLib.Pattern.Pipeline;
using QS.GameLib.Rx.Relay;
using QS.GameLib.Util;
using UnityEngine;

namespace QS.Executor.Domain.Handler
{
    /// <summary>
    /// ��ISubpipelineĿ�Ĳ�ͬ, IsubPipeline Ŀ����Ϊ�˼������е�Pipeline, Ϊ�˸���
    /// ��ߵ��Ӵ�������Ϊ�˾ۺ��߼������һ��ָ��Ĵ���, �費��Ҫ���������󴫵��ɸ�
    /// ָ��Ĵ���������,һ���ǲ��õ�
    /// ״̬������Ҫ�����Ӵ�����
    /// </summary>
    class MoveInstructionHandler : AbstractHandler
    {
        readonly IPipelineContext _pipeCtx = IPipelineContext.New();

        public MoveInstructionHandler(IRelayExecutor executor, 
                    Transform transform, Animator animator) : base(executor)
        {
            _pipeCtx.Pipeline.AddLast("Control", new MoveInstructionControlHandler(transform));
            _pipeCtx.Pipeline.AddLast("Anim", new MoveInstructionAnimHandler(animator));

        }

        public override void Read(IPipelineHandlerContext context, object msg)
        {
            if(!ReflectionUtil.IsChildOf<IMoveInstruction>(msg))
            {
                context.Write(msg);
                return;
            }

            var i = (MoveInstruction)msg;

            var controlMsg = new MoveInstructionControlHandler.Msg()
            {
                horizontal = i.Horizontal,
                vertical = i.Vertical,
                jump = i.Jump,
                baseRight = i.BaseRight,
                baseforword = i.Baseforword,
                baseUp = i.BaseUp
            };
          _pipeCtx.InBound(controlMsg);
        }

    }
}