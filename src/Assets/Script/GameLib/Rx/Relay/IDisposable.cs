

namespace QS.GameLib.Rx.Relay
{
    /// <summary>
    /// ͬ���������, ��ʱҲ��ҪһЩ�򵥵ı�ѹ, 0-1, �������߹ر������ӵĲ���
    /// ������һ���þͻָ���������, ??���Ե�ֻҪ������������Resume�Ϳ����������µ�����ֵ��
    /// </summary>
    public interface IDisposable
    {
     
        bool Disposed { get; set; }
        bool Paused { get; set; }
    }
}