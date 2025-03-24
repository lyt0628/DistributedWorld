

namespace QS.Combat
{
    /// <summary>
    /// 是Combator实体类下的一个值类型
    /// </summary>
    public interface ITalent
    {
        /// <summary>
        /// 生命天赋
        /// </summary>
        public int life { get; }

        /// <summary>
        /// 魔法天赋
        /// </summary>
        public int magic { get; }

        /// <summary>
        /// 天赋下的生命值加成，被视为战斗者的原始生命值
        /// </summary>
        /// <param name="original"></param>
        /// <returns></returns>
        float LifePlus(float originalLife);

        /// <summary>
        /// 天赋值下的魔力量加成
        /// </summary>
        /// <param name="originalMana"></param>
        /// <returns></returns>
        float MagicPlus(float originalMana);
    }
}