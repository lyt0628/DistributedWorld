using GameLib;
using QS.API;
using System.Collections.Generic;
using UnityEngine;

using GameLib.DI;
using GameLib.Impl;
using QS.Impl;
using QS.Impl.Data;
using QS.API.DataGateway;
using QS.Impl.Service;
using QS.Impl.Service.DTO;

public class GameManager : SingtonBehaviour<GameManager>
{
    public IMessager GlobalMessager { get { return _globalMessager; } }
    public IDIContext GlobalDIContext { get { return _globalDIContext; } }

    private readonly List<IGameManager> _managers = new();
    private readonly IMessager _globalMessager = new Messager();
    private readonly IDIContext _globalDIContext = IDIContext.New();
    [Injected]
    private readonly ILifecycleProivder lifecycle;

    public override void Awake()
    {
        base.Awake();
        GlobalDIContext.BindInstance(DINames.SINGLE_GAME_MANAGER, this)
                        // Unity Service
                        // Base Component
                        .BindInstance(LifecycleProvider.Instance)
                        // Gloal Setting
                        .Bind(typeof(GlobalPhysicSetting))
                        // Data Layer
                        .Bind(typeof(PlayerLocationData))
                        .Bind(typeof(PlayerInputData))
                        .Bind(typeof(PlayerCharacterData))
                        .Bind(typeof(InventoryData))
                        // Gata Gateway Layer
                        .BindInstance(
                            new InventoryItemActiveRepoWrapper<InventoryItemActiveRecord>(
                                new InventoryItemActiveRepo()))
                        // Service Layer
                        .Bind(typeof(PlayerControllService))
                        .Bind(typeof(CharacterTranslationDTO));

        GlobalDIContext.Inject(this);

        _managers.Add(new ViewManager());


        _managers.ForEach(manager => { manager.Startup(); });


        lifecycle.Request(Lifecycles.Update, () =>
        {
            _managers.ForEach(manager => { manager.Update(); });
        });
    }
 
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Q))
        {
            Application.Quit();
            Debug.Log("Quit");
        }
    }

    // 什么时候会使用 SPI,问题是什么时候不能使用 DI
    // 在实例绑定时, 实例 可能运行时改变的话 不能使用DI, 因为 DI不可抢占
    public T GetManager<T>()
    {
        return (T)_managers.Find(manager => manager is T );
    }

}
