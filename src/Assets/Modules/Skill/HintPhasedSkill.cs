using QS.Api.Skill.Domain;
using QS.Chara.Domain;
using QS.Common.FSM;


namespace QS.Skill
{
    /// <summary>
    /// 在前一个技能 Shutdown的时候，切换到下一个技能的开始
    /// </summary>
    public abstract class HintPhasedSkill : BaseSkill
    {
        protected abstract IState<SkillStage> GetPhaseState(int currentPhase, SkillStage stage);
        /// <summary>
        /// 子类需要提供给我，到底可不可以切换技能这个事情
        /// 如果可以切换的话，就直接切过去了，
        /// 在第前一个SKill Shutdown 的时候，如果说你认为可以触发技能了，我就切过去
        /// </summary>
        /// <returns></returns>


        public int CurrentSkill { get; protected set; } = 0;
        protected bool canSwitchSkill = false;

        protected HintPhasedSkill(Character chara) : base(chara)
        {
        }

        protected abstract bool CanSwitchPhaseOnPlayerHint(int currentSkill);
        public virtual int PhaseCount { get; }
        // 如果允许在技能结束时候也发动技能，就要在这里做逻辑了
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
            // 总之，要先切出第一个技能
            base.SwitchTo(to);
            var isLastSkill = CurrentSkill == PhaseCount - 1;
            // 前一个SKill 失去控制的时候，如果可以切技能(前提是不是最后一个技能)
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
                // 如果是最后一个技能shutdown了，直接回复到第一个技能
                CurrentSkill = 0;
                canSwitchSkill = false;
            }
        }

        private void ToNextPhase()
        {
            // 立即切出Shutdown
            GetState(SkillStage.Shutdown).Exit();

            // 立即切到下一个技能
            DoSwitchSkillPhase();
            CurrentSkill++;
            CurrentSkill %= PhaseCount;

            CurrentState = SkillStage.Precast;
            GetState(SkillStage.Precast).Enter();

            //SwitchTo(SkillStage.Precast);
        }

        /// <summary>
        /// 完成切换技能的实际任务，一般是触发下一的状态的状态机
        /// </summary>
        protected virtual void DoSwitchSkillPhase()
        {

        }

        /// <summary>
        /// 动态切换状态，来表示技能的切换
        /// </summary>
        /// <param name="state"></param>
        /// <returns></returns>
        public override IState<SkillStage> GetState(SkillStage state)
        {
            return GetPhaseState(CurrentSkill, state);
        }

    }
}