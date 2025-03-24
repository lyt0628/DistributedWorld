

using QS.Common.FSM;

namespace QS.Agent
{
    /// <summary>
    /// �����Ľ�ɫ״̬��
    /// ԭ�� ״̬=>ָ��
    /// ���� ״̬=>�ж�=>ָ��
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