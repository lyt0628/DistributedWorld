

using QS.Api.Skill.Domain;
using System;
using System.Collections;
using System.Collections.Generic;
using Tomlet.Attributes;

namespace QS.Skill.SimpleSkill
{
    class PhasedSimpleSkill : ISimpleSkill, IEnumerable<ISimpleSkill>
    {
        List<ISimpleSkill> Phases { get; } = new();

        public void AddPhase(ISimpleSkill skillAbility)
        {
            Phases.Add(skillAbility);
        }

        public IEnumerator<ISimpleSkill> GetEnumerator()
        {
            return ((IEnumerable<ISimpleSkill>)Phases).GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return ((IEnumerable)Phases).GetEnumerator();
        }

        public PhasedSimpleSkill(ISkillKey skillKey)
        {
            this.Key = skillKey;
        }
        public ISkillKey Key { get; }

        public string[] Handlers => Array.Empty<string>();

        public Dictionary<string, object> ResourceMap => default;
    }
}