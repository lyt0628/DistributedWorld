using QS.Api.Executor.Domain;
using QS.Api.Skill.Domain;
using QS.Chara.Domain;
using QS.Common.FSM;
using System;
using System.Collections.Generic;
using UnityEngine.Assertions;

namespace QS.Skill
{
    class HintPhasedSkillBuilder : IHintPhasedSkillBuilder, ISkillBuilderContext
    {
        readonly ICurrySkillStageFactory stageFactory;
        const int SKILL_STAGE_SIZE = 5;
        public HintPhasedSkillBuilder()
        {
            stageFactory = new CurryStageFacotry(this);
        }
        int phaseCount = 0;
        List<IState<SkillStage>> stages;
        List<float> animOffsets;
        DDHintPhasedSkill m_PhasedSkill;
        //Func<BaseSkill, int, SkillStage, bool> onInterruptCB;

        public BaseSkill Skill => m_PhasedSkill;

        public SkillStage CurrentStage { get; private set; }


        public IHintPhasedSkillBuilder Begin(Character chara, Func<IInstruction, bool> canHandleFunc, out ICurrySkillStageFactory stageFactory)
        {
            phaseCount = 0;
            stageFactory = this.stageFactory;
            stages = new List<IState<SkillStage>>();
            animOffsets = new List<float>();
            m_PhasedSkill = new DDHintPhasedSkill(chara, canHandleFunc);

            return this;
        }

        //public IHintPhasedSkillBuilder OnInterruptCB(Func<BaseSkill, int, SkillStage, bool> onInterruptCB)
        //{
        //    this.onInterruptCB = onInterruptCB;
        //    return this;
        //}

        public IHintPhasedSkillBuilder NewPhase(float animOffset)
        {
            phaseCount++;
            animOffsets.Add(animOffset);
            CurrentStage = SkillStage.Precast;
            return this;
        }


        public IFSM<SkillStage> Build()
        {
            Assert.IsTrue(phaseCount > 0);
            Assert.IsTrue(stages.Count / SKILL_STAGE_SIZE == phaseCount);
            Assert.IsTrue(animOffsets.Count == phaseCount);

            m_PhasedSkill.m_PhaseCount = phaseCount;
            m_PhasedSkill.Stages = stages;
            m_PhasedSkill.AnimOffsets = animOffsets;
            //m_PhasedSkill.OnInterruptCB = onInterruptCB;
            return m_PhasedSkill;
        }
        public IHintPhasedSkillBuilder Precast(IState<SkillStage> state)
        {
            CurrentStage = SkillStage.Casting;
            stages.Add(state);
            return this;
        }

        public IHintPhasedSkillBuilder Casting(IState<SkillStage> state)
        {
            CurrentStage = SkillStage.Postcast;
            stages.Add(state);
            return this;
        }

        public IHintPhasedSkillBuilder Postcast(IState<SkillStage> state)
        {
            CurrentStage = SkillStage.Shutdown;
            stages.Add(state);
            return this;
        }

        public IHintPhasedSkillBuilder Shutdown(IState<SkillStage> state)
        {
            CurrentStage = SkillStage.Recoveried;
            stages.Add(state);
            return this;
        }
        public IHintPhasedSkillBuilder Recoveried(IState<SkillStage> state)
        {
            stages.Add(state);
            return this;
        }


    }
}