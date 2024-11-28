using QS.Domain.Item;
using System.Collections.Generic;

namespace QS.Impl.Data.Store
{
   internal class Inventory
    {
        readonly Dictionary<string, Cell> cells = new();

        public void Add(Item item)
        {
            var uuid = item.UUID;
            if (cells.ContainsKey(uuid))
            {
                cells[uuid].count++;
            }
            else
            {
                cells[uuid] = new Cell(item);
            }
        }

        public void Remove(string uuid)
        {
            cells[uuid].count--;
            if (cells[uuid].count == 0)
            {
                cells.Remove(uuid);
            }
        }

        public IList<Item> GetAll()
        {
            var items = new List<Item>();
            foreach (var cell in cells)
            {
                for (var i = 0; i < cell.Value.count; i++)
                {
                    items.Add(cell.Value.item);
                }
            }
            return items;
        }

        private class Cell
        {
            public Cell(Item item)
            {
                this.item = item;
            }
            public Item item;
            public int count = 1;
        }

    }
}
