
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
    public IInventory Inventory => _inventory;

    private IView _inventoryView;

    //private IView _tabView;
    public void Startup()
    {

        _inventoryView.OnInit();

        _inventory = new Inventory();
        _inventory.AddListener(_inventoryView.OnModelChanged);

        var itemPool = GameManager.Instance.GetManager<ItemManager>().ItemPool;
        _inventory.AddItem(itemPool.Get(GameConstants.REALITY_PIECE));

    }

    public void Update()
    {
        Debug.Log("InventoryManage is running");
    }
}