

namespace QS.Combat
{
    /// <summary>
    /// ��Combatorʵ�����µ�һ��ֵ����
    /// </summary>
    public interface ITalent
    {
        /// <summary>
        /// �����츳
        /// </summary>
        public int life { get; }

        /// <summary>
        /// ħ���츳
        /// </summary>
        public int magic { get; }

        /// <summary>
        /// �츳�µ�����ֵ�ӳɣ�����Ϊս���ߵ�ԭʼ����ֵ
        /// </summary>
        /// <param name="original"></param>
        /// <returns></returns>
        float LifePlus(float originalLife);

        /// <summary>
        /// �츳ֵ�µ�ħ�����ӳ�
        /// </summary>
        /// <param name="originalMana"></param>
        /// <returns></returns>
        float MagicPlus(float originalMana);
    }
}