

namespace QS.Api.Skill.Domain
{
    /// <summary>
    /// һ�������ܹ���⼸���׶Σ�
    /// �����Ƿֶμ��ܣ���Ϊ��ĳһ�׶�Ҳ�Ǻ���ġ�
    /// </summary>
    public enum SkillStage
    {
        /// <summary>
        /// ���ܵ�ǰҡ����
        /// </summary>
        Precast,
        /// <summary>
        /// ���ܷ�������ʾ��ʼ�����˺����
        /// </summary>
        Casting,
        /// <summary>
        /// ���ܺ�ҡ����ʾ�˺��������
        /// </summary>
        Postcast,
        /// <summary>
        /// ��ʾ���ܽ��������������ɽ��ϣ����ҽ��뼼�ܽ�ֱ�׶�
        /// </summary>
        Shutdown,
        /// <summary>
        /// �Ѿ��ظ�����ʾ�������������Ľ���һ����ƶ�����
        /// </summary>
        Recoveried
    }

}
