



using GameLib;
using System;
using System.Collections.Generic;

namespace QS.API
{
    interface IInventory : IListenable<Action>
    {
        void AddItem(IItem item, int count);
        void AddItem(IItem item);

        List<string> GetItemNames();
        IItem GetItemEntry(string name);
        int GetItemCount(string name);
        int GetItemCountByUUID(string uuid);

    }
}