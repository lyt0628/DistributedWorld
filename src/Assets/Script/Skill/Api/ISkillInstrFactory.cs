

using QS.Api.Executor.Domain;
using QS.Api.Skill.Domain;

namespace QS.Api.Skill
{
    public interface ISkillInstrFactory
    {
        IInstruction Create(ISkill skill);

    }
}