

using GameLib.DI;

namespace QS.Api.Common
{
    /// <summary>
    /// Ψһһ�����Խ��|�� Unity �����Global������ģ�K�Ĺ��ܳ���
    /// �Ҳ�ϲ�����ȫ�Ŀ�ܣ�ÿ��СС���������һ����������ЧӦ
    /// </summary>
    public interface ITrunkGlobal
    {

        IDIContext Context { get; }
    }
}