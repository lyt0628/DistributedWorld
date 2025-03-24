


namespace QS.Combat
{
    /// <summary>
    /// ������ Entity
    /// </summary>
    public interface IBuff
    {
        /// <summary>
        /// UUID ������Ӧ�ó�����Ψһ
        /// </summary>
        string UUID { get; }
        /// <summary>
        /// ԭʼֵ���͵����Ļ���ֵ������һ���µĻ���ֵ
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