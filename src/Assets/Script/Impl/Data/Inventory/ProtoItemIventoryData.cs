


using QS.Api.Data;
using QS.Domain.Item;
using System.Collections.Generic;
using UnityEngine;

namespace QS.Impl.Data
{
    internal class ProtoItemInventoryData : IInventoryData
    {
        readonly Dictionary<IItem, int> items = new();

        public void Add(IItem item)
        {
            Debug.Log("ProtoItem Added");
            if (items.ContainsKey(item))
            {
                items[item] += 1;
            }
            else
            {
                items[item] = 1;
            }
        }


        public void Remove(IItem item)
        {
            if (items[item] > 0)
            {
                items[item] -= 1;
            }
        }

    }
}