using QS.Chara.Domain;
using QS.Skill.SimpleSkill;

namespace QS.Api.Skill.Domain
{
     interface ISimpleSkillSubHandler : ISimpleSkillConfigurationParser
    {
        void PreLoad(Character chara, ISimpleSkillHandler handler);
        void OnPrecastEnter(Character chara, ISimpleSkillHandler handler);
        void OnPrecastExit(Character chara, ISimpleSkillHandler handler);
        void OnCastingEnter(Character chara, ISimpleSkillHandler handler);
        void OnCastingExit(Character chara, ISimpleSkillHandler handler);
        void OnPostcastEnter(Character chara, ISimpleSkillHandler handler);
        void OnPostcastExit(Character chara, ISimpleSkillHandler handler);
    }
}