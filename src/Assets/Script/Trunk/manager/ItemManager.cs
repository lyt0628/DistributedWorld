
using UnityEngine;


using GameLib.View;
using QS.API;
using GameLib;
using QS;


class ItemManager : IGameManager
{
    public ManagerStatus Status => throw new System.NotImplementedException();

    private IMessager _messager = new Messager();
    public IMessager Messager => _messager;

    private readonly SimpleItemPool _itemPool = new ();
    public SimpleItemPool ItemPool => _itemPool;

    //private IView _tabView;
    public void Startup()
    {
          _itemPool.Add(new RealityPieceModel());
    }

    public void Update()
    {
    }
}