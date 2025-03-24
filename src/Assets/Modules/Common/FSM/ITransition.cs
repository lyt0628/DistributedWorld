


using System;

namespace QS.Common.FSM
{
    /// <summary>
    /// ��ʾ״̬��Ǩ�ơ�״̬��Ǩ���ǵ����Ĺ��̣���Exit֮��
    /// Enter ֮ǰ������׶�Ҫ����״̬��ת������״̬ת�����֮ǰ��
    /// ������Ϊ��ǰһ���׶ε�״̬
    /// </summary>
    public interface ITransition<TState> where TState : struct, Enum
    {
        Func<bool> Condition { get; }
        TState Target { get; }
        /// <summary>
        /// ��ʾ״̬ת�ƵĹ��̣�����true��ʾת�����
        /// Ǩ�ƿ��Ա�ʾ�ܶ࣬���缼���ͷ�ʱ�Ե��˵ĳ����޸�
        /// </summary>
        /// <returns></returns>
        bool Transite();
        void Begin();

        public static ITransition<TState>[] Empty = new ITransition<TState>[0];
    }
}