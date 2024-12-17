

using QS.Api.Executor.Domain;
using QS.Api.Skill.Domain;
using QS.Api.Skill.Service;
using QS.Skill.Domain.Instruction;

namespace QS.Skill.Service
{
    class SkillInstrFactory : ISkillInstrFactory
    {
        public IInstruction Simple(string skillNo, string skillName)
        {
            var k = ISkillKey.New(skillNo, skillName);
            return new SimpleSkillInstr(k);
        }
    }
}