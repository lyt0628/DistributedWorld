

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
    /// ���ڵ�phasedskill ֻ֧���ڼ����ڲ� hit��һ�׶�
    /// 
    /// ״̬û�����õ�DDû����
    /// </summary>
    sealed class DDHintPhasedSkill : HintPhasedSkill
    {
        const int SKILL_STAGE_SIZE = 5;

        readonly Func<IInstruction, bool> canHandleFunc;
        //public Func<BaseSkill, int, SkillStage, bool> OnInterruptCB;
        public override int PhaseCount => m_PhaseCount;
        internal int m_PhaseCount;
        public List<float> AnimOffsets { get; internal set; }
        public List<IState<SkillStage>> Stages { get; internal set; }


        public DDHintPhasedSkill(Character chara,
                                 Func<IInstruction, bool> canHandleFunc,
                                 List<IState<SkillStage>> states,
                                 List<float> animOffsets)
            : base(chara)
        {
            // ������Ŀ������Ϊ�׶εı���
            Assert.IsTrue(states.Count >= SKILL_STAGE_SIZE
                          && states.Count % SKILL_STAGE_SIZE == 0);
            m_PhaseCount = states.Count / SKILL_STAGE_SIZE;
            this.Stages = states;
            this.canHandleFunc = canHandleFunc;
            this.AnimOffsets = animOffsets;
        }

        public DDHintPhasedSkill(Character chara, Func<IInstruction, bool> canHandleFunc)
            : base(chara)
        {
            this.canHandleFunc = canHandleFunc;
        }

        //[Obsolete]
        //public override void OnInterrupt()
        //{
        //    if (OnInterruptCB == null || !Casting) return;
        //    var canInt = OnInterruptCB.Invoke(this, CurrentSkill, CurrentState);
        //    if (canInt)
        //    {
        //        canSwitchSkill = false;
        //        while (Casting)
        //        {
        //            NextStage();
        //        }
        //        CurrentSkill = 0;
        //        CurrentState = SkillStage.Recoveried;
        //    }
        //}

        public override bool CanHandle(IInstruction instruction) => canHandleFunc(instruction);

        protected override bool CanSwitchPhaseOnPlayerHint(int currentSkill)
        {
            // ��֧�ּ�����Ĵ���������ָ�մ���precast��ʱ��
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