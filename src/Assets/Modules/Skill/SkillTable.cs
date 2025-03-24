

using GameLib.DI;
using QS.Api.Executor.Domain;
using QS.Api.Skill.Domain;
using QS.Common.FSM;
using System;
using System.Collections.Generic;
using System.Linq;

namespace QS.Skill
{
    [Scope(Value = ScopeFlag.Prototype)]
    class SkillTable : ISkillTable
    {        readonly List<IFSM<SkillStage>> m_Skills = new();

        public void AddSkill(IFSM<SkillStage> handler)
        {
            m_Skills.Add(handler);
        }

        public void RemoveSkill(IFSM<SkillStage> handler)
        {
            m_Skills.Remove(handler);
        }

        public bool TryHande(IInstruction instruction)
        {
            return m_Skills.Any(sk => sk.TryHande(instruction));
        }
    }
}