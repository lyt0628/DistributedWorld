

using QS.Api.Executor.Domain;
using QS.Api.Skill.Domain;
using QS.Chara.Domain;
using QS.GameLib.Pattern.Message;
using UnityEngine;

namespace QS.Api.Skill.Service
{
    public interface ISkillAblityFactory
    {
        ISimpleSkillHandler Simple(Character character, ISkillKey key);

    }
}