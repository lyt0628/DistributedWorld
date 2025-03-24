

using QS.Api.WorldItem.Domain;
using QS.WorldItem;

namespace QS.Player.Inventory
{
    abstract class InventoryService : IInventoryService
    {

        public abstract void AddItem(IItem item);
        public abstract IItem[] GetItemsByTimeDesc();
        public abstract IProp[] GetProps();
        public abstract IWeapon[] GetWeapons();
    }
}