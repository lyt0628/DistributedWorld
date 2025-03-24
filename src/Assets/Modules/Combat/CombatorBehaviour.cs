

using QS.Api.Executor.Domain;
using QS.Common;
using UnityEngine;
using UnityEngine.Assertions;
using UnityEngine.Events;

namespace QS.Combat
{



    /// <summary>
    /// ECS ģʽ�����������������.��Ϊ�ͻ��ˣ���Ҫ��Ӧ���ݱ仯
    /// </summary>
    public class CombatorBehaviour : MonoBehaviour, ICombatComp
    {
        readonly CombatSystem m_System = CombatSystem.Get(ServiceType.Local);

        /// <summary>
        /// ����ɫ����ʱ��Ļص�
        /// </summary>
        public UnityEvent OnDead { get; } = new();

        /// <summary>
        /// ����ɫ��ս�����ݷ����仯ʱ��Ļص�
        /// </summary>
        public UnityEvent OnStateChanged { get; } = new();
        public UnityEvent OnHit { get; } = new();

        /// <summary>
        /// ���ݣ���ʾ��������ս������
        /// </summary>
        Combator combator;

        /// <summary>
        /// �����ṩ���ݷ��ʽ�
        /// </summary>
        public ICombator Combator => combator;

        /// <summary>
        /// �����˺��Ľӿڣ�����Ϊ�ڲ��ģ�����ֻ�� CombatSystem ���Է�������ӿ�
        /// ���������Ľ���ͨ������ģʽ���������ϵͳ���߼�ͨ��ֱ�ӵ�API ����
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