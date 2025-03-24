using QS.Api.Skill.Domain;
using QS.Common.FSM;
using System;

namespace QS.Skill
{


    /// <summary>
    /// ������������Ķ���ʹ�����ģʽ����
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
        /// �����ǲ����������ģ����Կ����붯��ʧ�⣬����״̬ʱ�������鶯���Ƿ���ȷ����
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