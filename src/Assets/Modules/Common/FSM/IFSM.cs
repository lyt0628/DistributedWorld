using System;

namespace QS.Common.FSM
{
    /// <summary>
    /// 简单有限状态机的基本接口,
    /// QS 的Handler 得支持命令接口
    /// </summary>
    /// <typeparam name="TState"></typeparam>
    public interface IFSM<TState> : IHandler where TState : struct, Enum
    {


        TState DefaultState { get; }
        TState CurrentState { get; }
        /// <summary>
        /// 是否正在执行状态迁移
        /// </summary>
        bool Transiting { get; }
        ITransition<TState> ActiveTransition { get; }

        IState<TState> GetState(TState state);

        /// <summary>
        /// 切换状态，不考虑迁移
        /// </summary>
        /// <param name="to"></param>
        void SwitchTo(TState to);

    }

}