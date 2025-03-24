using QS.Api.Executor.Domain;
using UnityEngine;

namespace QS.Agent
{
    /// <summary>
    /// ָʾ�ƄӵĽ�ɫ�����b���Ƅ�AI���㷨 
    /// ����A*֮���
    /// ����Ѱ·�����
    /// </summary>
    public interface ISteering
    {
        /// <summary>
        /// Agent ��Ŀ���ƶ�λ��
        /// </summary>
        Vector3 Destination { get; set; }
        /// <summary>
        /// �������ָ���ٶȵķ�ʽ
        /// </summary>
        bool Run { get; set; }
        /// <summary>
        /// Ϊ�˵���Ŀ��λ���ڱ�֡��Ҫָ����ָ��
        /// </summary>
        /// <returns></returns>
        IInstruction GetTranslateInstr();
    }
}