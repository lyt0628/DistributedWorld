

using QS.Api.Executor.Domain;
using QS.GameLib.Pattern.Pipeline;
using System;

namespace QS.Api.Skill.Domain
{
    public interface ISimpleSkillHandler : IInstructionHandler
    {
        public void AddSubHandler(ISimpleSkillSubHandler subHandler);
        
    }
}