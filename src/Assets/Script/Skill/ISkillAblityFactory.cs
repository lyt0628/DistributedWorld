

using QS.Api.Executor.Domain;
using QS.Api.Skill.Domain;
using QS.Chara.Domain;
using QS.GameLib.Pattern.Message;
using UnityEngine;

namespace QS.Api.Skill
{
    public interface ISkillAblityFactory
    {
        IInstructionHandler Create(Character character, ISkill skill);
    }

}