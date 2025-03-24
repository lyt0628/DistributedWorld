

using QS.Common.FSM;

namespace QS.Agent
{
    /// <summary>
    /// 基本的角色状态。
    /// 原本 状态=>指令
    /// 现在 状态=>行动=>指令
    /// </summary>
    public abstract class AgentStateBase
        : IState<AgentState>
    {
        public AgentTemplate agent;
        protected AgentStateBase(AgentTemplate agent)
        {
            this.agent = agent;
        }

        public abstract AgentState State { get; }

        public ITransition<AgentState>[] Transitions => ITransition<AgentState>.Empty;

        public virtual void Enter()
        {
        }

        public virtual void Exit()
        {
        }

        public virtual void Process()
        {
        }

    }
}