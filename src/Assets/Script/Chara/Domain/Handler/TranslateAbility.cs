



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
    /// 与ISubpipeline目的不同, IsubPipeline 目的是为了集成已有的Pipeline, 为了复用
    /// 这边的子处理器是为了聚合逻辑来完成一个指令的处理, 需不需要将命令往后传递由该
    /// 指令的处理器决定,一般是不用的
    /// 状态尽量不要存在子处理器
    /// 
    /// 這個Handler名稱應該叫做 CharaTranslationHandler
    /// </summary>
    class TranslateAbility : AbstractHandler
    {
        readonly IPipelineContext _pipeCtx = IPipelineContext.New();

        public TranslateAbility(IRelayExecutor executor, 
                    Transform transform, Animator animator) : base(executor)
        {
            _pipeCtx.Pipeline.AddLast("Control", new MoveInstructionControlHandler(transform));
            _pipeCtx.Pipeline.AddLast("Anim", new MoveInstructionAnimHandler(animator));

        }

        public override void Read(IPipelineHandlerContext context, object msg)
        {
            if(!ReflectionUtil.IsChildOf<ITranslateInstr>(msg))
            {
                context.Write(msg);
                return;
            }

            var i = (TranslateInstr)msg;

            var controlMsg = new MoveInstructionControlHandler.Msg()
            {
                horizontal = i.Horizontal,
                vertical = i.Vertical,
                jump = i.Jump,
                dash = i.Dash,
                baseRight = i.BaseRight,
                baseforword = i.Baseforword,
                baseUp = i.BaseUp
            };
          _pipeCtx.InBound(controlMsg);
        }

    }
}