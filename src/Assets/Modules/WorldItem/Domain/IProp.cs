

using QS.Api.WorldItem.Domain;

namespace QS.WorldItem
{
    /// <summary>
    /// �������ǿ�ʹ�õģ��������ϸ���ú�����ϸ��
    /// </summary>
    public interface IProp : IItem
    {
        bool isDepletable { get; }
        void Use();
    }
}