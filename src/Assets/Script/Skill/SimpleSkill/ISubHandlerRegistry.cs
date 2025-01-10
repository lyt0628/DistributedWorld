

using QS.Api.Skill.Domain;

namespace QS.Skill.SimpleSkill
{
    interface ISubHandlerRegistry
    {
        void Register(string name, ISimpleSkillSubHandler handler);

        ISimpleSkillSubHandler GetSubHandler(string name);
    }
}