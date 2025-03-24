


namespace QS.Combat
{
    /// <summary>
    /// 增益物 Entity
    /// </summary>
    public interface IBuff
    {
        /// <summary>
        /// UUID 在整个应用程序中唯一
        /// </summary>
        string UUID { get; }
        /// <summary>
        /// 原始值，和到如今的积累值。返回一个新的积累值
        /// </summary>
        /// <param name="original"></param>
        /// <param name="acc"></param>
        /// <returns></returns>
        float AttackPowerPlus(float original, float acc);

        float MagicPowerPlus(float original, float acc);

        float DefencePlus(float original, float acc);

        float MagicDefencePlus(float original, float acc);

    }
}