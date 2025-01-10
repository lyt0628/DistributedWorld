


using QS.Api.Skill;
using QS.Api.Skill.Domain;
using System.Collections.Generic;

namespace QS.Skill
{
    class DefaultSkillRepo : ISkillRepo
    {
        readonly List<ISkill> skills = new();

        public void AddSkill(ISkill skill)
        {
            skills.Add(skill);
        }

        public ISkill GetSkill(string skillNo)
        {
            return skills.Find(sk=>sk.Key.No == skillNo);
        }

        public ISkill GetSkill(ISkillKey key)
        {
            return skills.Find(sk=>sk.Key == key);
        }
    }
}