

using QS.Api.Executor.Domain;
using QS.Api.Skill.Domain;
using QS.Chara.Domain;
using QS.Common.FSM;
using System;

namespace QS.Skill
{
    /// <summary>
    /// 简单的技能重写技能状态。复杂的技能直接重写技能类
    /// 技能状态用子类沙盒模式编写,或者直接从接口开始重写
    /// </summary>
    /// /这个状态的任务是触发动画,实例化特效
    /// 用于连接服务器的 在 postcast 方法中
    /// 客户端发送给服务端，服务端返回信息，都以流的方式
    /// 客户端这边的技能模块只负责表现，和告诉服务器，谁发动什么技能，攻击到了谁
    /// 
    /// 约定一下，这些 DD 开头的类都是数据驱动的类 Data Driven
    /// 也就是不是定义，而是通过提供数值和闭包的创建方式。因为闭包，所以很多父类的功能就
    /// 用不了，如果是简单技能的话，或是，能力允许来源与外部环境，就没问题了。
    sealed class DDSimpleSkill : BaseSkill
    {

        public DDSimpleSkill(Character chara,
                             Func<IInstruction, bool> canHandleFunc,
                             IState<SkillStage> precastStage,
                             IState<SkillStage> castingStage,
                             IState<SkillStage> postcastStage,
                             IState<SkillStage> shutdownState = null)
            : base(chara)
        {
            this.canHandleFunc = canHandleFunc;
            this.PrecastStage = precastStage;
            this.CastingStage = castingStage;
            this.PostcastStage = postcastStage;
            this.ShutdownState = shutdownState;
        }

        public DDSimpleSkill(Character chara, Func<IInstruction, bool> canHandleFunc)
            : base(chara)
        {
            this.canHandleFunc = canHandleFunc;
        }


        readonly Func<IInstruction, bool> canHandleFunc;
        public IState<SkillStage> PrecastStage { get; internal set; }
        public IState<SkillStage> CastingStage { get; internal set; }
        public IState<SkillStage> PostcastStage { get; internal set; }
        public IState<SkillStage> ShutdownState { get; internal set; }
        public IState<SkillStage> RecoveriedStage { get; internal set; } = IState<SkillStage>.Unit;


        public override bool CanHandle(IInstruction instruction) => canHandleFunc(instruction);

        public override IState<SkillStage> GetState(SkillStage state)
        {
            return state switch
            {
                SkillStage.Precast => PrecastStage,
                SkillStage.Casting => CastingStage,
                SkillStage.Postcast => PostcastStage,
                SkillStage.Shutdown => ShutdownState,
                SkillStage.Recoveried => RecoveriedStage,
                _ => throw new System.NotImplementedException(),
            };
        }

    }

}
