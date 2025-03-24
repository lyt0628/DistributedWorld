


using QS.Api.WorldItem.Domain;
using QS.WorldItem;

namespace QS.Player
{
    /// <summary>
    /// Ҳһ��������Ҫ���ص�ҲҪ�������ġ�
    /// �ѷ�������ͬ�������أ����ڱ��صĿͻ�����˵�������ε���������
    /// ��ô�������û����ԣ���һ����ֻ�����첽���ˡ�
    /// </summary>
    public interface IInventory
    {
        void Add(IItem item);
        IWeapon[] GetWeapons();
        IProp[] GetProps();
        INote[] GetNotes();
        IMaterial[] GetMaterials();
        IItem Get(string uuid);
        bool Contains(IItem item);
        bool Remove(IItem item);
    }


}