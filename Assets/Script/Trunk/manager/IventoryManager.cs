
using UnityEngine;


using GameLib.View;
using QS.API;
using GameLib;


class InventoryManager : IGameManager
{
    public ManagerStatus Status => throw new System.NotImplementedException();

    private IMessager _messager = new Messager();
    public IMessager Messager => _messager;

    private IInventory _inventory;

    private IView _inventoryView;

    //private IView _tabView;
    public void Startup()
    {

        _inventoryView = new InventoryView();
        _inventoryView.OnInit();

        _inventory = new Inventory();
        _inventory.AddListener(_inventoryView.OnModelChanged);


        //_tabView = new TabView();
        //_tabView.Show();
    }

    public void Update()
    {
        _inventoryView.OnUpdate();
        Debug.Log("InventoryManage is running");
    }
}