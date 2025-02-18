

namespace QS.Common.ComputingService
{
    /// <summary>
    ///  �����ʵ����ֻ��ά����һ��״̬��
    ///  �����ʹ��һ���ֵ�洢����ã���Ϊ���ڵķ���̫������
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public abstract class BaseProxy<T> : IProxy<T> where T : ISnapshot
    {

        public void UpdateSnapshot(T snapshot)
        {
            Snapshot = snapshot;
        }

        public T Snapshot { get; private set; }
    }
}