


using QS.Api.Skill.Domain;
using System;
using System.Collections.Generic;
using System.Linq;

using UnityEngine;

namespace QS.Skill.SimpleSkill
{
    class DefaultSimpleSkill : ISimpleSkill
    {
        public DefaultSimpleSkill(ISkillKey key, string[] handlers) {
            Key = key;
            ResourceMap = new();
            Handlers = handlers;
        }
        public ISkillKey Key { get; }

        public Dictionary<string, object> ResourceMap { get; }

        public string[] Handlers { get; }
    }
}