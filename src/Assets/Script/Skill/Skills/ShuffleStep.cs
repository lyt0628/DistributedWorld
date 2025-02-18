


using GameLib.DI;
using QS.Api.Skill.Domain;
using QS.Skill.Domain.Handler;
using QS.Skill.SimpleSkill;

namespace QS.Skill.Skills
{
    [Scope(Value = ScopeFlag.Sington, Lazy = false)]
    class ShuffleStep : SimpleSkillSubHandlerAdapter 
    {
        [Injected]
        public ShuffleStep(DefaultSkillRepo skillRepo, ISubHandlerRegistry registry) {
            var skillKey = ISkillKey.New("00003", "ShuffleStep");
            var skill = new SimpleSkill.DefaultSimpleSkill(skillKey, new[] { "ShuffleStep" });
            registry.Register("ShuffleStep", this);
            skillRepo.AddSkill(skill);
        }

    }
}