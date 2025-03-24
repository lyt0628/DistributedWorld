using QS.Api.Skill.Domain;
using QS.Common.FSM;
using System;

namespace QS.Skill
{


    /// <summary>
    /// 技能这种外面的东西使用外观模式更好
    /// </summary>
    public abstract class BaseSkillState : IState<SkillStage>
    {

        protected readonly BaseSkill skill;
        public abstract SkillStage State { get; }
        public virtual bool Interrutable { get; } = true;

        protected BaseSkillState(BaseSkill skill)
        {
            this.skill = skill ?? throw new ArgumentNullException(nameof(skill));
        }


        public virtual void OnAnimMsg(string msg) { }



        public ITransition<SkillStage>[] Transitions => ITransition<SkillStage>.Empty;

        /// <summary>
        /// 进入是不依赖动画的，所以可能与动画失衡，进入状态时，必须检查动画是否正确进入
        /// </summary>
        public virtual void Enter()
        {
        }

        public virtual void Exit()
        {
        }

        public virtual void OnInterrupt()
        {
        }

        public virtual void Process()
        {
        }
    }

}