

using QS.Api.Skill.Domain;
using QS.Api.Skill.Domain.Instruction;
using QS.Skill.SimpleSkill;

namespace QS.Skill.Domain.Instruction
{
    class CastSkillInstr : ICastSkillInstr
    {
        public CastSkillInstr(ISkill skill) 
        {
            Skill = skill;
        }

        public ISkill Skill { get; }
    }
}