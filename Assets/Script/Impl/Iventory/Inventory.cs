
using GameLib;
using QS;
using QS.API;
using System;
using System.Collections.Generic;
using UnityEditor.Experimental.GraphView;

class Inventory : IInventory 
{
    private Dictionary<string, IItemCell> _itemMap = new();


    #region [[Listener]]
    private ListenableWrapper<Action> _listneners = new();

    public void AddListener(Action listener)
    {
        _listneners.AddListener(listener);
    }
    public void RemoveListener(Action listener)
    {
        _listneners.RemoveListener(listener);
    }

    #endregion


    public void AddItem(IItem item, int count)
    {
        if (_itemMap.ContainsKey(item.Name))
        {
            _itemMap[item.Name].Add(count);
        } else
        {
            var cell = new ItemCell()
            {
                Item = item,
            };
            _itemMap[item.Name] = cell;
        }
    }

    public void AddItem(IItem item)
    {
        AddItem(item, 1);
    }

    public IItem GetItemEntry(string name)
    {
        return _itemMap[name].Item;
    }
    public int GetItemCount(string name)
    {
        if (_itemMap.ContainsKey(name))
        {
            return _itemMap[name].GetCount();
        }
        return 0;
    }

    public int GetItemCountByUUID(string uuid)
    {
        throw new System.NotImplementedException();
    }

    public List<string> GetItemNames()
    {
        return new List<string>(_itemMap.Keys);
    }

    
}
