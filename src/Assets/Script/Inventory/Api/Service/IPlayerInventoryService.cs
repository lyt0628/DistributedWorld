namespace QS.Api.Inventory.Service
{

    /// <summary>
    /// IPlayerInventoryService Provides Operation on InventoryStore.
    /// <seealso cref="Data.IInventoryData"/>
    /// </summary>
    public interface IPlayerInventoryService
    {
        public void AddItem(string itemName);
        public void RemoveItem(string itemUUID);

    }
}