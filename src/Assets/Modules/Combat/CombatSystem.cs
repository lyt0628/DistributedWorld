

using QS.Common;

namespace QS.Combat
{
    /// <summary>
    /// ս��ϵͳ������Զ�˺ͱ��ؼ��������
    /// </summary>
    abstract class CombatSystem : ICombatSystem
    {

        public static CombatSystem Get(ServiceType type)
        {
            return type switch
            {
                ServiceType.Local => new LocalCombatSystem(),
                ServiceType.Remote => throw new System.NotImplementedException(),
                _ => throw new System.NotImplementedException(),
            };
        }

        /// <summary>
        /// �����˺��Ľӿ�, ֱ��ʹ������������ýӿ�.
        /// ���ǻ�����Ϸģ�͵ģ�����ǻ��ڽ�ɫ��ÿ����ɫ����ͳһ�ļ���������ô����Ͷ���
        /// �����������ͨ�ü��ܾͺ�
        /// ��֮����Ҫ�� ��������ݳ������ڼ��㣬���ء�
        /// 
        /// ����Ǳ��ؼ���Ļ���ֱ�����û����ļ����������ˣ�
        /// �����Զ�̵ģ��ͰѼ��ܴ��봫��Զ�̼���
        /// </summary>
        public abstract void TakeDamage(CombatorBehaviour attacked, CombatorBehaviour attacker, ISkill skill);

        public abstract void TakeHeal(CombatorBehaviour combator, float hpDelta);


    }
}