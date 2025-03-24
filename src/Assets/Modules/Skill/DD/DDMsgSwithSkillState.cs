using QS.Api.Skill.Domain;
using System;

namespace QS.Skill
{
    sealed class DDMsgSwithSkillState : MsgSwitchSkillState
    {
        readonly Action OnEnter;
        readonly Action OnProcess;
        readonly Action OnExit;
        readonly Action OnInterruptCB;
        readonly bool m_Interruptable;
       

        public DDMsgSwithSkillState(BaseSkill skill,
                                    SkillStage state,
                                    string animTrigger = "",
                                    string animNext = "SK_Next",
                                    Action onEnter = null,
                                    Action onProcess = null,
                                    Action onExit = null,
                                    bool interruptable = false,
                                    Action onInterruptCB = null) : base(skill)
        {
            this.AnimTrigger = animTrigger;
            this.State = state;
            OnEnter = onEnter;
            OnProcess = onProcess;
            OnExit = onExit;
            m_Interruptable = interruptable;
            OnInterruptCB = onInterruptCB;
            AnimTrigger = animTrigger;
            NextMsg = animNext;
        }

        protected override string AnimTrigger { get; }
        protected override string NextMsg { get; }
        public override SkillStage State { get; }

        public override void Exit()
        {
            base.Exit();
            OnExit?.Invoke();
        }

        public override void Process()
        {
            base.Process();
            OnProcess?.Invoke();
        }

        public override void Enter()
        {
            base.Enter();
            OnEnter?.Invoke();
        }
        public override void OnInterrupt()
        {
            base.OnInterrupt();
            OnInterruptCB?.Invoke();
        }
        public override bool Interrutable => m_Interruptable;
    }
}