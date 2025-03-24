
using GameLib.DI;
using QS.Api.Executor.Domain;
using QS.Common.FSM;
using System.Collections.Generic;
using System.Linq;

namespace QS.Common
{
    [Scope(Value = ScopeFlag.Prototype)]
    public class HandlerGroup : IHandlerGroup
    {
        readonly List<IHandler > m_Handlers = new();
        public void Add(IHandler skill)
        {
            m_Handlers.Add(skill);
        }

        public bool Contains(IHandler handler)
        {
            return m_Handlers.Contains(handler);
        }

        public void Remove(IHandler skill)
        {
            m_Handlers.Remove(skill);
        }

        public bool TryHande(IInstruction instruction)
        {
            return m_Handlers.Any(sk => sk.TryHande(instruction));
        }
    }
}