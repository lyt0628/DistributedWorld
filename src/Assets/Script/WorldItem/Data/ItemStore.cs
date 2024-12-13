
using QS.Api.WorldItem.Domain;
using QS.WorldItem.Domain;
using System.Collections.Generic;
using System.Linq;




namespace QS.Impl.Data.Store
{
    class ItemSotre
    {
        readonly Dictionary<string, Item> stores = new();

        public void Add(Item item)
        {
            stores.Add(item.UUID, item);
        }

        public void Remove(string uuid)
        {
            stores.Remove(uuid);
        }

        public bool Contains(string uuid)
        {
            return stores.ContainsKey(uuid);
        }

        public bool Find(string uuid, out Item item)
        {
            return stores.TryGetValue(uuid, out item);
        }

        public IList<Item> GetAll()
        {
            return stores.Values.ToList();
        }
        public IList<Item> GetAll(ItemType type)
        {
            return stores.Values.ToList().Where(i => i.Type == type).ToList();
        }

    }
}