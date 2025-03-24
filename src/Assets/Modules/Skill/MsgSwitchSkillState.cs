using QS.Api.Skill.Domain;

namespace QS.Skill
{
    /// �����𼶼�ǿ������ʹ�����ģʽ����������
    public abstract class MsgSwitchSkillState : BaseSkillState
    {
        protected MsgSwitchSkillState(BaseSkill skill) : base(skill) { }

        const string DefaultNextMessage = "SK_Next";

        protected virtual string AnimTrigger { get; } = string.Empty;
        protected virtual string NextMsg { get; } = DefaultNextMessage;

        /// <summary>
        /// TODO ��鶯��
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