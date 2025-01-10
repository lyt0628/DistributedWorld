
using QS.Api.Skill.Domain;
using System.Collections.Generic;

namespace QS.Skill.SimpleSkill
{
    public interface ISimpleSkill : ISkill
    {
        string[] Handlers { get; }
        Dictionary<string, object> ResourceMap { get; }
    }
}