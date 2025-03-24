


using UnityEngine.Events;

namespace QS.Combat
{
    /// <summary>
    /// 战斗数据的持有者，计算由系统类的计算
    /// </summary>
    public interface ICombator
    {
        /// <summary>
        /// 添加增益物品
        /// </summary>
        /// <param name="buff"></param>
        void AddBuff(IBuff buff);
        /// <summary>
        /// 移除增益物品
        /// </summary>
        /// <param name="buff"></param>
        void RemoveBuff(IBuff buff);
        /// <summary>
        /// 用于向外通知状态变化的接口
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
        /// 战斗者的天赋属性
        /// </summary>
        ITalent talent { get; }

    }
}