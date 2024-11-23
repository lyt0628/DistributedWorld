

using QS.Api.Data;
using QS.Domain.Item;
using System.Collections.Generic;

namespace QS.Impl.Data
{
    internal class StandaloneItemInventory : IInventoryData
    {
        readonly List<IItem> items = new();

        public void Add(IItem item)
        {
            items.Add(item);
        }

        public void Remove(IItem item)
        {
            items.Remove(item);
        }
    }

}