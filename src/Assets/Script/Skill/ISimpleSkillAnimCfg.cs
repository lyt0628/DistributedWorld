

using QS.Api.Skill.Domain;

namespace QS.Api.Skill.Predefine
{
    /// <summary>
    /// DefaultSimpleSkill �������s�����@�����QΨһ�ͺã���Ҽs���á�
    /// �������ܵ�Key������.
    /// </summary>
    public interface ISimpleSkillAnimCfg
    {
        /// <summary>
        /// <para>>
        /// MSG_Format: SK[id]-[name]-[stage]-[patch]
        /// </para>
        /// <para>>
        /// The patch part may not exits. e.g. SK00001-FireBall-Precast.

        /// </para>
        /// </summary>
        /// <remarks>
        /// More Example:
        /// <list type="bullet">
        /// <item>SK00002-IceWave-Casting-LV20. The animation changed when Skill level above 20.</item>
        /// <item>SK00002-IceWave-Casting-V1. The animation changed once.</item>
        /// </list>
        /// </remarks>
        const string MSG_Format = "SK{0}_{1}_{2}";

        /// ������������A�Σ����Nÿ�����A�ζ�ҕ��һ������̎���ȷ��f ���ք�����ͨ���� ��̖001 ���A��1, ���A�β����^��λ
        /// SK00001_01_SwordTap
        const string Trigger_Format = "SK{0}_{1}";

        /// <summary>
        /// GetMsg GetMsg of a Create Skill
        /// </summary>
        /// <param name="no"></param>
        /// <param name="name"></param>
        /// <param name="stage"></param>
        /// <returns></returns>
        string GetMsg(ISkillKey key, SimpleSkillStage stage); // �Ȳ������ļ�, û�ҵ�������Ĭ��Լ���õ�����

        string GetAnimTrigger(ISkillKey key);
            
    }
}
