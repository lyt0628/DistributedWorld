

namespace QS.Agent
{
    /// <summary>
    /// AI ״̬
    /// </summary>
    public enum AgentState
    {
        /// <summary>
        /// û�н���ս��״̬�����ɻ
        /// </summary>
        Free,
        /// <summary>
        /// ����״̬����������ң�׼���������й���
        /// </summary>
        Focus,
        /// <summary>
        /// ����״̬����������ң����������
        /// </summary>
        Around,
        /// <summary>
        /// ����״̬����Ծ�Ľ��й���
        /// </summary>
        Active,
        /// <summary>
        /// ����״̬���������ҹ������������
        /// </summary>
        Defence,
        /// <summary>
        /// �������Ҹ�����Σ�գ���ҪԶ�����
        /// </summary>
        FarAway,
        /// <summary>
        /// ��ˮ�Ľ׶Σ����������·������һ��ʼ�ü���
        /// </summary>
        Pending,
    }
}