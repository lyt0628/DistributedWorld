using System;

namespace QS.Common.FSM
{

    /// <summary>
    /// ��ֻ����һ���ӿ�, ��¶��ȥ�Ķ����ǽӿ�
    /// ��״ֻ̬Ӧ�����������������ж�
    /// </summary>
    /// <typeparam name="TState"></typeparam>
    public interface IState<TState> where TState : struct, Enum
    {
        void Enter();
        void Exit();
        void Process();
        TState State { get; }

        /// <summary>
        /// Ǩ���б�ÿ֡��ʼ���Ǩ���������������������Ǩ�ƹ�ȥ
        /// </summary>
        ITransition<TState>[] Transitions { get; }

        public static IState<TState> Unit { get; } = new UnitState();

        public sealed class UnitState : IState<TState>
        {
            public TState State => default;

            public ITransition<TState>[] Transitions => ITransition<TState>.Empty;

            public void Enter()
            {
            }
            public void Exit()
            {
            }
            public void Process()
            {
            }
        }
    }

}