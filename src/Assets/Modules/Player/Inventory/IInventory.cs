


using QS.Api.WorldItem.Domain;
using QS.WorldItem;

namespace QS.Player
{
    /// <summary>
    /// 也一样，我想要本地的也要服务器的。
    /// 把服务器的同步到本地，对于本地的客户端来说，就屏蔽掉服务器了
    /// 那么，对于用户而言，这一步就只能是异步的了。
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