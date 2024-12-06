using GameLib.DI;
using QS.Api;
using QS.Combat.Domain;
using QS.Common;
using QS.Control;
using QS.GameLib.Pattern;
using QS.GameLib.Pattern.Message;
using QS.GameLib.Rx;
using QS.GameLib.Rx.Relay;
using QS.Impl;
using QS.Inventory;
using QS.WorldItem;
using System;
using System.Collections.Generic;
using UnityEngine;

public class TrunkGlobal : SingtonBehaviour<TrunkGlobal>
{
    public IMessager GlobalMessager { get { return _globalMessager; } }
    public IDIContext DI { get { return _globalDIContext; } }

    private readonly List<IGameManager> _managers = new();
    private readonly IMessager _globalMessager = new Messager();
    private readonly IDIContext _globalDIContext = IDIContext.New();
    [Injected]
    private readonly ILifecycleProivder lifecycle;

    public override void Awake()
    {
        base.Awake();

        CommonGlobal.Instance.ProvideBinding(DI);
        WorldItemGlobal.Instance.ProvideBinding(DI);
        InventoryGlobal.Instance.ProvideBinding(DI);
        ControlGlobal.Instance.ProvideBinding(DI);

        DI
          .BindInstance(TrunkDINames.Trunk_GLOBAL, this)
          .Bind<DefaultCombator>(ScopeFlag.Prototype);

        DI.Inject(this);


        //_managers.Add(new ViewManager());

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
        return (T)_managers.Find(manager => manager is T);
    }




}
