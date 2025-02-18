using QS.Api.Executor.Domain;
using QS.Api.Executor.Domain.Instruction;
using QS.Chara.Abilities;
using QS.Executor.Domain.Instruction;
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
    /// 
    /// �@��Handler���Q��ԓ���� CharaTranslationHandler
    /// </summary>
    class DefaultCharaControlAbility0 : AbstractHandler, ICharaConrolAbility
    {
        readonly IPipelineContext _pipeCtx = IPipelineContext.New();
        static MoveInstructionControlHandler.Msg voidMsg = new MoveInstructionControlHandler.Msg()
        {
            horizontal = 0f,
            vertical = 0f,
            jump = false,
            dash = false,
            baseRight = Vector3.right,
            baseforword = Vector3.forward,
            baseUp = Vector3.up,
        };

        public bool Enabled { get; set; }

        public CharaControlState State => throw new System.NotImplementedException();

        public DefaultCharaControlAbility0(IRelayExecutor executor, 
                    Transform transform, Animator animator) : base(executor)
        {
            _pipeCtx.Pipeline.AddLast("Control", new MoveInstructionControlHandler(transform));
            _pipeCtx.Pipeline.AddLast("Anim", new MoveInstructionAnimHandler(animator));
        }

        public override void Read(IPipelineHandlerContext context, object msg)
        {
            if(!ReflectionUtil.IsChildOf<ICharaControlInstr>(msg))
            {
                context.Write(msg);
                return;
            }
            if (!Enabled) 
            {
                _pipeCtx.InBound(voidMsg);
                return;
            }
                
            var i = (CharaControlInstr)msg;

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