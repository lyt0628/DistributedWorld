


using System;
using System.Collections.Generic;
using System.Linq;

namespace QS.GameLib.Util
{
    public class SimpleStore<TKey, TValue>
    {
        readonly Dictionary<TKey, TValue> stores = new();

        public void Add(TKey key ,TValue item)
        {
            stores.Add(key, item);
        }

        public void Remove(TKey key)
        {
            stores.Remove(key);
        }

        public bool Contains(TKey key)
        {
            return stores.ContainsKey(key);
        }

        public bool Find(TKey key, out TValue value)
        {
            return stores.TryGetValue(key, out value);
        }

        public IList<TValue> GetAll()
        {
            return stores.Values.ToList();
        }
        public IList<TValue> GetAll(Func<TValue, bool> condition)
        {
            return stores.Values.ToList().Where(i => condition(i)).ToList();
        }
    }
}