using QS.Api.WorldItem.Domain;
using System.Collections.Generic;

namespace QS.WorldItem
{
    // ����һ�����εăȴ�惦����Ҫ�}�s����
    class ItemFlyweightRepo
    {
        readonly Dictionary<string, IItem> m_items = new();

        public void Add(IItem flyweight)
        {
            m_items.Add(flyweight.name, flyweight);
        }

        public IItem Get(string name)
        {
            if (m_items.ContainsKey(name))
            {

                return m_items[name];
            }
            else
            {
                throw new System.ArgumentException($"Item not found! {name}");
            }
        }
    }
}