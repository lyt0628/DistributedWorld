

using QS.Api.Executor.Domain;
using QS.Common;
using UnityEngine;
using UnityEngine.Assertions;
using UnityEngine.Events;

namespace QS.Combat
{



    /// <summary>
    /// ECS 模式，由组件来持有数据.作为客户端，需要响应数据变化
    /// </summary>
    public class CombatorBehaviour : MonoBehaviour, ICombatComp
    {
        readonly CombatSystem m_System = CombatSystem.Get(ServiceType.Local);

        /// <summary>
        /// 当角色死亡时候的回调
        /// </summary>
        public UnityEvent OnDead { get; } = new();

        /// <summary>
        /// 当角色的战斗数据发生变化时候的回调
        /// </summary>
        public UnityEvent OnStateChanged { get; } = new();
        public UnityEvent OnHit { get; } = new();

        /// <summary>
        /// 数据，表示这个组件的战斗数据
        /// </summary>
        Combator combator;

        /// <summary>
        /// 向外提供数据访问接
        /// </summary>
        public ICombator Combator => combator;

        /// <summary>
        /// 接受伤害的接口，定义为内部的，所以只有 CombatSystem 可以访问这个接口
        /// 组件和组件的交互通过命令模式来，组件和系统的逻辑通过直接的API 访问
        /// </summary>
        internal void TakeDamage(float hpDelta)
        {
            Assert.IsTrue(hpDelta > 0);
            combator.Hp -= hpDelta;
            if (combator.Hp <= 0)
            {
                combator.Hp = 0;
                OnDead.Invoke();
            }

            OnStateChanged.Invoke();
            //Debug.Log($".................attacked {gameObject.name}");
        }

        internal void TakeHeal(float hpDelta)
        {
            Assert.IsTrue(hpDelta > 0);
            combator.Hp += hpDelta;
            if (combator.Hp > combator.MaxHP) combator.Hp = combator.MaxHP;
            OnStateChanged.Invoke();
        }

        private void Start()
        {
            combator = new Combator(100, 100, 100, 30, 5, 100, 100);
        }


        public bool TryHande(IInstruction instruction)
        {
            if (instruction is AttackedInstr attackedInstr)
            {
                HandleAttacked(attackedInstr);
                return true;
            }
            return false;
        }

        void HandleAttacked(AttackedInstr instr)
        {
            m_System.TakeDamage(this, instr.Attacker, instr.Skill);
        }

    }
}