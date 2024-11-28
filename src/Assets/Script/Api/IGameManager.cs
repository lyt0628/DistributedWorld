using QS.GameLib.Pattern.Message;

namespace QS.Api
{
    /// <summary>
    /// ��Ϸ������, ��Ϊȫ�ַ�������ṩ, ����֤�������������ڶ�����
    /// ����MonoBehaviour���ǻ����������������
    /// һ����˵,Ҳֻ����Щ���������ڵĻ����������Ѱ�����
    /// </summary>
    public interface IGameManager : IMessagerProvider
    {
        ManagerStatus Status { get; }

        void Startup();
        void Update();
    }

}