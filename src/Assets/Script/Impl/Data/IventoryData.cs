


using QS.API.Data;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace QS.Impl.Data
{
    class InventoryData : IInventoryData
    {
        Dictionary<IItem, int> items = new ();
        public void Add(IItem item)
        {
            Debug.Log("Add Item" + item.Name);
            //if (items.ContainsKey(item)) {
            //    items[item] += 1;
            //}
            //else
            //{
            //    items.Add(item, 1);
            //}
        }

        public void Remove(IItem item)
        {
            //items[item] -= 1;
            //if (items[item] == 0)
            //{
            //    items.Remove(item);
            //}
        }
    }
}