using QS.Api.Skill.Domain;
using QS.Chara.Domain;
using QS.Common.FSM;


namespace QS.Skill
{
    /// <summary>
    /// ��ǰһ������ Shutdown��ʱ���л�����һ�����ܵĿ�ʼ
    /// </summary>
    public abstract class HintPhasedSkill : BaseSkill
    {
        protected abstract IState<SkillStage> GetPhaseState(int currentPhase, SkillStage stage);
        /// <summary>
        /// ������Ҫ�ṩ���ң����׿ɲ������л������������
        /// ��������л��Ļ�����ֱ���й�ȥ�ˣ�
        /// �ڵ�ǰһ��SKill Shutdown ��ʱ�����˵����Ϊ���Դ��������ˣ��Ҿ��й�ȥ
        /// </summary>
        /// <returns></returns>


        public int CurrentSkill { get; protected set; } = 0;
        protected bool canSwitchSkill = false;

        protected HintPhasedSkill(Character chara) : base(chara)
        {
        }

        protected abstract bool CanSwitchPhaseOnPlayerHint(int currentSkill);
        public virtual int PhaseCount { get; }
        // ��������ڼ��ܽ���ʱ��Ҳ�������ܣ���Ҫ���������߼���
        protected override void OnPlayerHint()
        {
            if (!canSwitchSkill)
            {
                canSwitchSkill = CanSwitchPhaseOnPlayerHint(CurrentSkill);
            }
        }

        public override void SwitchTo(SkillStage to)
        {

            //Debug.Log($"Phase Count {PhaseCount}");
            //Debug.Log($"Current Skill {currentSkill}, To Stage {to}, Can Switch {canSwitchSkill}");
            // ��֮��Ҫ���г���һ������
            base.SwitchTo(to);
            var isLastSkill = CurrentSkill == PhaseCount - 1;
            // ǰһ��SKill ʧȥ���Ƶ�ʱ����������м���(ǰ���ǲ������һ������)
            if (!isLastSkill && to is SkillStage.Shutdown)
            {
                if (canSwitchSkill)
                {
                    //Debug.Log($"Combo!! Current {currentSkill}");
                    canSwitchSkill = false;
                    ToNextPhase();
                }
                else
                {
                    CurrentSkill = 0;
                    canSwitchSkill = false;
                }
            }
            else if (isLastSkill && to is SkillStage.Recoveried)
            {
                // ��������һ������shutdown�ˣ�ֱ�ӻظ�����һ������
                CurrentSkill = 0;
                canSwitchSkill = false;
            }
        }

        private void ToNextPhase()
        {
            // �����г�Shutdown
            GetState(SkillStage.Shutdown).Exit();

            // �����е���һ������
            DoSwitchSkillPhase();
            CurrentSkill++;
            CurrentSkill %= PhaseCount;

            CurrentState = SkillStage.Precast;
            GetState(SkillStage.Precast).Enter();

            //SwitchTo(SkillStage.Precast);
        }

        /// <summary>
        /// ����л����ܵ�ʵ������һ���Ǵ�����һ��״̬��״̬��
        /// </summary>
        protected virtual void DoSwitchSkillPhase()
        {

        }

        /// <summary>
        /// ��̬�л�״̬������ʾ���ܵ��л�
        /// </summary>
        /// <param name="state"></param>
        /// <returns></returns>
        public override IState<SkillStage> GetState(SkillStage state)
        {
            return GetPhaseState(CurrentSkill, state);
        }

    }
}