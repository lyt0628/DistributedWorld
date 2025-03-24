using System;

namespace QS.Common.FSM
{

    /// <summary>
    /// 它只能是一个接口, 暴露出去的东西是接口
    /// 子状态只应当在生命周期内有行动
    /// </summary>
    /// <typeparam name="TState"></typeparam>
    public interface IState<TState> where TState : struct, Enum
    {
        void Enter();
        void Exit();
        void Process();
        TState State { get; }

        /// <summary>
        /// 迁移列表，每帧开始检测迁移条件，如果条件成立就迁移过去
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