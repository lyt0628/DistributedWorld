

namespace QS.Api.Service
{

    /// <summary>
    /// IPlayerInventoryService Provides Operation on Inventory.
    /// <seealso cref="Data.IInventoryData"/>
    /// </summary>
    public interface IPlayerInventoryService
    {
        public void AddItem(string itemName);
        public void RemoveItem(string itemUUID);

    }
}