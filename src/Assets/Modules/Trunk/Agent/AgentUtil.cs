

using QS.Common.FSM;
using System;

namespace QS.Agent
{
    static class AgentUtil
    {
        public static void LoadLuaAgentState(AgentTemplate agent, AgentState state, string address, Action<IState<AgentState>> CB)
        {
            var loadFocus = new LuaAgentStateLoadOp(agent, state, address);
            loadFocus.Invoke();
            loadFocus.Completed += (h) => CB(h.Result);
        }
    }
}