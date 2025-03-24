
using QS.Common;
using UnityEngine.Events;

namespace QS.Combat
{
    /// <summary>
    /// ս�����
    /// </summary>
    public interface ICombatComp : IHandler
    {
        /// <summary>
        /// ����ɫ����ʱ��Ļص�
        /// </summary>
        public UnityEvent OnDead { get; }
        /// <summary>
        /// ����ɫ��ս�����ݷ����仯ʱ��Ļص�
        /// </summary>
        public UnityEvent OnStateChanged { get; }
        public UnityEvent OnHit { get; }
        public ICombator Combator { get; }
    }
}