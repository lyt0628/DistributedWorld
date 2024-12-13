

using QS.Api.Skill.Domain.Instruction;

namespace QS.Skill.Domain.Instruction
{
    class SimpleSkillInstr : ISimpleSkillInstr
    {
        public SimpleSkillInstr(string skillNo, string skillName) 
        { 
            SkillNo = skillNo;
            SkillName = skillName;
        }
        public string SkillNo { get; }

        public string SkillName { get; }
    }
}