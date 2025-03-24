


using UnityEngine.Events;

namespace QS.Combat
{
    /// <summary>
    /// ս�����ݵĳ����ߣ�������ϵͳ��ļ���
    /// </summary>
    public interface ICombator
    {
        /// <summary>
        /// ���������Ʒ
        /// </summary>
        /// <param name="buff"></param>
        void AddBuff(IBuff buff);
        /// <summary>
        /// �Ƴ�������Ʒ
        /// </summary>
        /// <param name="buff"></param>
        void RemoveBuff(IBuff buff);
        /// <summary>
        /// ��������֪ͨ״̬�仯�Ľӿ�
        /// </summary>
        UnityEvent OnDataChanged { get; }

        float Hp { get; }
        float MaxHP { get; }
        float Mana { get; }
        float MaxMana { get; }

        float Stamina { get; }
        float MaxStamina { get; }

        float AttackPower { get; }
        float BuffedAttackPower { get; }

        float MagicPower { get; }

        float BuffedMagicPower { get; }

        float Defence { get; }
        float BuffedDefence { get; }

        float MagicDefence { get; }

        float BuffedMagicDefence { get; }
        /// <summary>
        /// ս���ߵ��츳����
        /// </summary>
        ITalent talent { get; }

    }
}