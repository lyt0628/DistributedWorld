


using System;

namespace QS.Common.FSM
{
    /// <summary>
    /// 表示状态的迁移。状态的迁移是单独的过程，在Exit之后
    /// Enter 之前，这个阶段要进行状态的转换，在状态转换完成之前，
    /// 都还认为是前一个阶段的状态
    /// </summary>
    public interface ITransition<TState> where TState : struct, Enum
    {
        Func<bool> Condition { get; }
        TState Target { get; }
        /// <summary>
        /// 表示状态转移的过程，返回true表示转换完成
        /// 迁移可以表示很多，比如技能释放时对敌人的朝向修改
        /// </summary>
        /// <returns></returns>
        bool Transite();
        void Begin();

        public static ITransition<TState>[] Empty = new ITransition<TState>[0];
    }
}