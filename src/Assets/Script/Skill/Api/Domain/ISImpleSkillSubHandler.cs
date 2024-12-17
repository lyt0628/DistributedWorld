using QS.Chara.Domain;

namespace QS.Api.Skill.Domain
{
    public interface ISimpleSkillSubHandler
    {
        void OnPrecastEnter(Character chara);
        void OnPrecastExit(Character chara);
        void OnCastingEnter(Character chara);
        void OnCastingExit(Character chara);
        void OnPostcastEnter(Character chara);
        void OnPostcastExit(Character chara);
    }
}