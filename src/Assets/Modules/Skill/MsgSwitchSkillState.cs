using QS.Api.Skill.Domain;

namespace QS.Skill
{
    /// 与其逐级加强，不如使用外观模式来做更合适
    public abstract class MsgSwitchSkillState : BaseSkillState
    {
        protected MsgSwitchSkillState(BaseSkill skill) : base(skill) { }

        const string DefaultNextMessage = "SK_Next";

        protected virtual string AnimTrigger { get; } = string.Empty;
        protected virtual string NextMsg { get; } = DefaultNextMessage;

        /// <summary>
        /// TODO 检查动画
        /// </summary>
        /// <param name="msg"></param>
        public override void OnAnimMsg(string msg)
        {
            if (msg == NextMsg && skill.CurrentState == State)
            {
                skill.NextStage();
            }
        }

        public override void Enter()
        {
            base.Enter();
            if (State is SkillStage.Precast && !string.IsNullOrEmpty(AnimTrigger))
            {
                skill.Chara.Animator.SetTrigger(AnimTrigger);
            }
        }

    }
}