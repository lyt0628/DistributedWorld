

using QS.Api.Executor.Domain;
using QS.Api.Skill.Domain;
using QS.Chara.Domain;
using QS.Common.FSM;
using System;

namespace QS.Skill
{
    public interface IHintPhasedSkillBuilder
    {
        IHintPhasedSkillBuilder Begin(Character chara,
                                      Func<IInstruction, bool> canHandleFunc,
                                      out ICurrySkillStageFactory stageFacotry);
        IHintPhasedSkillBuilder NewPhase(float animOffset);
        IHintPhasedSkillBuilder Precast(IState<SkillStage> state);
        IHintPhasedSkillBuilder Casting(IState<SkillStage> state);
        IHintPhasedSkillBuilder Postcast(IState<SkillStage> state);
        IHintPhasedSkillBuilder Shutdown(IState<SkillStage> state);
        IHintPhasedSkillBuilder Recoveried(IState<SkillStage> state);
        IFSM<SkillStage> Build();
        //IHintPhasedSkillBuilder OnInterruptCB(Func<BaseSkill, int, SkillStage, bool> onInterruptCB);
    }
}