
using QS.Common;
using UnityEngine.Events;

namespace QS.Combat
{
    /// <summary>
    /// 战斗组件
    /// </summary>
    public interface ICombatComp : IHandler
    {
        /// <summary>
        /// 当角色死亡时候的回调
        /// </summary>
        public UnityEvent OnDead { get; }
        /// <summary>
        /// 当角色的战斗数据发生变化时候的回调
        /// </summary>
        public UnityEvent OnStateChanged { get; }
        public UnityEvent OnHit { get; }
        public ICombator Combator { get; }
    }
}