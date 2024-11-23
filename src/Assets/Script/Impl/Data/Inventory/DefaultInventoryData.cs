


using QS.Api.Data;
using QS.Domain.Item;

namespace QS.Impl.Data
{
    /// <summary>
    /// Default Implemetation of <see cref="IInventoryData"/>.
    /// </summary>
    public class DefaultInventoryData : IInventoryData
    {
        private readonly ProtoItemInventoryData protoItems = new();
        private readonly StandaloneItemInventory standloneItems = new();

        public void Add(IItem item)
        {
            if (item.UUID == "")
            {
                protoItems.Add(item);
            }
            else
            {
                standloneItems.Add(item);
            }
        }
        public void Remove(IItem item)
        {
            if (item.UUID == "")
            {
                protoItems.Remove(item);
            }
            else
            {
                standloneItems.Remove(item);
            }
        }
    }
}