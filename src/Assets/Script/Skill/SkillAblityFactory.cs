

using GameLib.DI;
using QS.Api.Executor.Domain;
using QS.Api.Skill;
using QS.Api.Skill.Domain;

using QS.Chara.Domain;
using QS.GameLib.Pattern.Message;
using QS.Skill.Handler;
using QS.Skill.SimpleSkill;
using UnityEngine;

namespace QS.Skill.Service
{
    class SkillAblityFactory : ISkillAblityFactory
    {

        public IInstructionHandler Create(Character character,ISkill skill)
        {

            if (skill is ISimpleSkill simpleSkill) { 
                return new SimpleSkillAblity(character, simpleSkill);
            }
            throw new System.Exception();
        }
    }
}