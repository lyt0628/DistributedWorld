using QS.Api.WorldItem.Domain;
using QS.WorldItem;

namespace QS.Api.WorldItem.Service
{
    /// <summary>
    /// �@������Q��һ�����S��WorldItem �ṩ��API ����ֻ��
    /// ������Ʒ�Ķ��x�����P��Ϣ��ԃ��
    /// �Լ��c��Ʒ���P�ķ��Ց������@�Y���F
    /// </summary>
    public interface IItemFactory
    {
        IWeapon CreateWeapon(string name); 
        IProp CreateProp(string name);
    }
}