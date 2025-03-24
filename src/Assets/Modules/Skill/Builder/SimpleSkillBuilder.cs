using QS.Api.Executor.Domain;
using QS.Api.Skill.Domain;
using QS.Chara.Domain;
using QS.Common.FSM;
using System;

namespace QS.Skill
{
    class SimpleSkillBuilder : ISimpleSKillBuilder, ISkillBuilderContext
    {
        readonly ICurrySkillStageFactory stageFactory;

        public SimpleSkillBuilder()
        {
            stageFactory = new CurryStageFacotry(this);
        }
        DDSimpleSkill m_SimpleSkill;
        public BaseSkill Skill => m_SimpleSkill;

        public SkillStage CurrentStage { get; private set; }


        public ISimpleSKillBuilder Begin(Character chara, Func<IInstruction, bool> canHandleFunc, out ICurrySkillStageFactory stageFactory)
        {
            stageFactory = this.stageFactory;
            m_SimpleSkill = new DDSimpleSkill(chara, canHandleFunc);
            CurrentStage = SkillStage.Precast;
            return this;
        }

        public IFSM<SkillStage> Build()
        {
            return m_SimpleSkill;
        }
        public ISimpleSKillBuilder Precast(IState<SkillStage> state)
        {
            CurrentStage = SkillStage.Casting;
            m_SimpleSkill.PrecastStage = state;
            return this;
        }

        public ISimpleSKillBuilder Casting(IState<SkillStage> state)
        {
            CurrentStage = SkillStage.Postcast;
            m_SimpleSkill.PostcastStage = state;
            return this;
        }

        public ISimpleSKillBuilder Postcast(IState<SkillStage> state)
        {
            CurrentStage = SkillStage.Shutdown;
            m_SimpleSkill.PostcastStage = state;
            return this;
        }

        public ISimpleSKillBuilder Shutdown(IState<SkillStage> state)
        {
            CurrentStage = SkillStage.Recoveried;
            m_SimpleSkill.ShutdownState = state;
            return this;
        }
        public ISimpleSKillBuilder Recoveried(IState<SkillStage> state)
        {
            CurrentStage = SkillStage.Casting;
            m_SimpleSkill.RecoveriedStage = state;
            return this;
        }


    }
}