
using QS.Api.Skill.Domain;
using QS.Chara.Domain;

namespace QS.Skill.Domain.Handler
{
    public abstract class AbstractSimpleSkillSubHandler : ISimpleSkillSubHandler
    {
        public virtual void OnCastingExit(Character chara)
        {
        }

        public virtual void OnCastingEnter(Character chara)
        {
        }

        public virtual void OnPostcastEnter(Character chara)
        {
        }

        public virtual void OnPostcastExit(Character chara)
        {
        }

        public virtual void OnPrecastEnter(Character chara)
        {
        }

        public virtual void OnPrecastExit(Character chara)
        {
        }
    }
}   