


using QS.Api.Skill.Domain;

namespace QS.Api.Skill
{
    public interface ISkillRepo
    {
        ISkill GetSkill(string skillNo);
        ISkill GetSkill(ISkillKey key);
    }
}