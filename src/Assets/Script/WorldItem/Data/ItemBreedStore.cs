
using QS.Api.WorldItem.Domain;
using QS.WorldItem.Domain;
using System.Collections.Generic;
using System.Linq;




namespace QS.Impl.Data.Store
{
    class ItemBreedSotre
    {
        readonly Dictionary<string, ItemBreed> stores = new();

        public void Add(ItemBreed weapon)
        {
            stores.Add(weapon.Name, weapon);
        }

        public void Remove(string name)
        {
            stores.Remove(name);
        }

        public bool Contains(string name)
        {
            return stores.ContainsKey(name);
        }

        public bool Find(string name, out ItemBreed weapon)
        {
            return stores.TryGetValue(name, out weapon);
        }

        public IList<ItemBreed> GetAll()
        {
            return stores.Values.ToList();
        }
        public IList<ItemBreed> GetAll(ItemType type)
        {
            return stores.Values.ToList().Where(i => i.Type == type).ToList();
        }

    }
}