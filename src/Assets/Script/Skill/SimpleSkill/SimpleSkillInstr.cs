

using QS.Api.Skill.Domain;
using QS.Api.Skill.Domain.Instruction;
using QS.Skill.SimpleSkill;

namespace QS.Skill.Domain.Instruction
{
    class SimpleSkillInstr : ISimpleSkillInstr
    {
        public SimpleSkillInstr(ISimpleSkill skill) 
        {
            Skill = skill;
        }

        public ISimpleSkill Skill { get; }
    }
}