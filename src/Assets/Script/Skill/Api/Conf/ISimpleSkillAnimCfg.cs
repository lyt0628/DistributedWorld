

using QS.Api.Skill.Domain;

namespace QS.Api.Skill.Predefine
{
    public interface ISimpleSkillAnimCfg
    {
        /// <summary>
        /// <para>>
        /// MSG_Format: SK[id]-[name]-[stage]-[patch]
        /// </para>
        /// <para>>
        /// The patch part may not exits. e.g. SK00001-FireBall-PrecastEnter.
        /// </para>
        /// </summary>
        /// <remarks>
        /// More Example:
        /// <list type="bullet">
        /// <item>SK00002-IceWave-CastingEnter-LV20. The animation changed when skill level above 20.</item>
        /// <item>SK00002-IceWave-CastingEnter-V1. The animation changed once.</item>
        /// </list>
        /// </remarks>
        const string MSG_Format = "SK{0}_{1}_{2}";

        const string Trigger_Format = "SK{0}_{1}";

        /// <summary>
        /// GetMsg GetMsg of a Simple Skill
        /// </summary>
        /// <param name="no"></param>
        /// <param name="name"></param>
        /// <param name="stage"></param>
        /// <returns></returns>
        string GetMsg(string no, string name, SimpleSkillStage stage); // �Ȳ������ļ�, û�ҵ�������Ĭ��Լ���õ�����

        string GetTrigger(string no, string name);
            
    }
}
