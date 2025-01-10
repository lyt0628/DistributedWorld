

using GameLib.DI;
using QS.Api.Executor.Domain;
using QS.Api.Skill;
using QS.Api.Skill.Domain;
using QS.Skill.Domain.Instruction;
using QS.Skill.SimpleSkill;

namespace QS.Skill.Service
{
    class SkillInstrFactory : ISkillInstrFactory
    {


        public IInstruction Create(ISkill skill)
        {
            
            if(skill is ISimpleSkill simpleSkill)
            {
                return new SimpleSkillInstr(simpleSkill);
            }
            throw new System.NotImplementedException();
        }
    }
}