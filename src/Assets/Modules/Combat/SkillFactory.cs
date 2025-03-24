

namespace QS.Combat
{
    public interface ISkillFactory
    {
        ISkill Skill01();
    }

    class SkillFactory : ISkillFactory
    {
        public ISkill Skill01()
        {
            return new Skill01();
        }
    }
}