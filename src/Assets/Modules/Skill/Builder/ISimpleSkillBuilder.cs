

using QS.Api.Executor.Domain;
using QS.Api.Skill.Domain;
using QS.Chara.Domain;
using QS.Common.FSM;
using System;

namespace QS.Skill

{
    public interface ISimpleSKillBuilder
    {
        ISimpleSKillBuilder Begin(Character chara, Func<IInstruction, bool> canHandleFunc, out ICurrySkillStageFactory stageFacotry);
        ISimpleSKillBuilder Precast(IState<SkillStage> state);
        ISimpleSKillBuilder Casting(IState<SkillStage> state);
        ISimpleSKillBuilder Postcast(IState<SkillStage> state);
        ISimpleSKillBuilder Shutdown(IState<SkillStage> state);
        ISimpleSKillBuilder Recoveried(IState<SkillStage> state);

        IFSM<SkillStage> Build();
    }
}