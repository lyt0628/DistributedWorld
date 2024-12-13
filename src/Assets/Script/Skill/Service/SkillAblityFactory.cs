

using QS.Api.Executor.Domain;
using QS.Api.Skill.Domain;
using QS.Api.Skill.Service;
using QS.Chara.Domain;
using QS.GameLib.Pattern.Message;
using QS.Skill.Handler;
using UnityEngine;

namespace QS.Skill.Service
{
    class SkillAblityFactory : ISkillAblityFactory
    {
        public ISimpleSkillHandler Simple(Character character, string skillNo, string skillName)
        {
            return new SimpleSkillAblity(character, skillNo, skillName);
        }
    }
}