


using QS.Api.WorldItem.Domain;
using QS.WorldItem;

namespace QS.Player
{
    public interface IInventory
    {
        void Add(IItem item);
        IWeapon[] GetWeapons();
        IProp[] GetProps();
        bool Contains(IItem item);
        bool Remove(IItem item);
    }


}