using System;

namespace QS.Common.FSM
{
    /// <summary>
    /// ������״̬���Ļ����ӿ�,
    /// QS ��Handler ��֧������ӿ�
    /// </summary>
    /// <typeparam name="TState"></typeparam>
    public interface IFSM<TState> : IHandler where TState : struct, Enum
    {


        TState DefaultState { get; }
        TState CurrentState { get; }
        /// <summary>
        /// �Ƿ�����ִ��״̬Ǩ��
        /// </summary>
        bool Transiting { get; }
        ITransition<TState> ActiveTransition { get; }

        IState<TState> GetState(TState state);

        /// <summary>
        /// �л�״̬��������Ǩ��
        /// </summary>
        /// <param name="to"></param>
        void SwitchTo(TState to);

    }

}