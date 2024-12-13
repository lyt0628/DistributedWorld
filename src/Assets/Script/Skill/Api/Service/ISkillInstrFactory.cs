

using QS.Api.Executor.Domain;

namespace QS.Api.Skill.Service
{
    public interface ISkillInstrFactory
    {
        IInstruction Simple(string skillNo, string skillName);

    }
}