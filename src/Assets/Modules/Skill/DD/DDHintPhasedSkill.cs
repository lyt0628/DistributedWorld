

using QS.Api.Executor.Domain;
using QS.Api.Skill.Domain;
using QS.Chara.Domain;
using QS.Common.FSM;
using System;
using System.Collections.Generic;
using UnityEngine.Assertions;

namespace QS.Skill
{
    /// <summary>
    /// 现在的phasedskill 只支持在技能内部 hit下一阶段
    /// 
    /// 状态没有内置的DD没有用
    /// </summary>
    sealed class DDHintPhasedSkill : HintPhasedSkill
    {
        const int SKILL_STAGE_SIZE = 5;

        readonly Func<IInstruction, bool> canHandleFunc;
        readonly Action doSwitchSkillPhaseCB;
        //public Func<BaseSkill, int, SkillStage, bool> OnInterruptCB;
        public override int PhaseCount => m_PhaseCount;
        internal int m_PhaseCount;
        public List<float> AnimOffsets { get; internal set; }
        public List<IState<SkillStage>> Stages { get; internal set; }


        public DDHintPhasedSkill(Character chara,
                                 Func<IInstruction, bool> canHandleFunc,
                                 List<IState<SkillStage>> states,
                                 List<float> animOffsets,
                                 Action doSwitchSkillPhaseCB = null)
            : base(chara)
        {
            // 技能数目必须能为阶段的倍数
            Assert.IsTrue(states.Count >= SKILL_STAGE_SIZE
                          && states.Count % SKILL_STAGE_SIZE == 0);
            m_PhaseCount = states.Count / SKILL_STAGE_SIZE;
            this.Stages = states;
            this.canHandleFunc = canHandleFunc;
            this.AnimOffsets = animOffsets;
            this.doSwitchSkillPhaseCB = doSwitchSkillPhaseCB;
        }

        public DDHintPhasedSkill(Character chara, Func<IInstruction, bool> canHandleFunc, Action doSwitchSkillPhaseCB)
            : base(chara)
        {
            this.canHandleFunc = canHandleFunc;
            this.doSwitchSkillPhaseCB = doSwitchSkillPhaseCB;
        }

        protected override void DoSwitchSkillPhase() => doSwitchSkillPhaseCB?.Invoke();

        public override bool CanHandle(IInstruction instruction) => canHandleFunc(instruction);

        protected override bool CanSwitchPhaseOnPlayerHint(int currentSkill)
        {
            // 不支持技能外的触发，尤其指刚触发precast的时候
            if (CurrentState is SkillStage.Shutdown or SkillStage.Recoveried) return false;
            return Chara.Animator.GetCurrentAnimatorStateInfo(0).normalizedTime >= AnimOffsets[currentSkill];
        }

        protected override IState<SkillStage> GetPhaseState(int currentPhase, SkillStage stage)
        {
            return stage switch
            {
                SkillStage.Precast => Stages[currentPhase * SKILL_STAGE_SIZE],
                SkillStage.Casting => Stages[currentPhase * SKILL_STAGE_SIZE + 1],
                SkillStage.Postcast => Stages[currentPhase * SKILL_STAGE_SIZE + 2],
                SkillStage.Shutdown => Stages[currentPhase * SKILL_STAGE_SIZE + 3],
                SkillStage.Recoveried => Stages[currentPhase * SKILL_STAGE_SIZE + 4],
                _ => throw new NotImplementedException(),
            };
        }
    }
}