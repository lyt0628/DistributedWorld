

using QS.Api.Skill.Domain;
using QS.Api.Skill.Domain.Instruction;

namespace QS.Skill.Domain.Instruction
{
    class SimpleSkillInstr : ISimpleSkillInstr
    {
        public SimpleSkillInstr(ISkillKey key) 
        {
            Key = key;
        }
        public ISkillKey Key { get; }
    }
}