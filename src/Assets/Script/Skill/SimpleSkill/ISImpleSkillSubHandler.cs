using QS.Chara.Domain;
using QS.Skill.SimpleSkill;
using Tomlet.Models;

namespace QS.Api.Skill.Domain
{
     interface ISimpleSkillSubHandler
    {
        void PreLoad(Character chara, ISimpleSkillAbility handler);
        void OnParseConfiguration(ISimpleSkill skill, TomlTable skillTable);
        void OnPrecast(Character chara, ISimpleSkillAbility handler);
        void OnCasting(Character chara, ISimpleSkillAbility handler);
        void OnPostcast(Character chara, ISimpleSkillAbility handler);
        void OnShutdown(Character chara, ISimpleSkillAbility handler);
        void OnCancel(Character chara, ISimpleSkillAbility handler);
        void OnInstructed(Character chara, ISimpleSkillAbility handler);
    }
}