

using UnityEngine.Events;

namespace QS.Api.Common
{
    /// <summary>
    /// Ψһһ�����Խ��|�� Unity �����Global������ģ�K�Ĺ��ܳ���
    /// </summary>
    public interface ITrunkGlobal : IInstanceProvider
    {
        /// <summary>
        /// ��������Ϸ��������ϣ�������Ϸ��ӭ����
        /// </summary>
        UnityEvent OnReady { get; }
    }
}