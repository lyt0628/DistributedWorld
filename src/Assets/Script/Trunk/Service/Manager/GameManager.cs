using GameLib;
using GameLib.View;
using GameLib.Util.Raycast;
using QS;
using QS.API;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Reflection;
using UnityEngine;
using GameLib.Uitl.RayCast;

using GameLib.DI;

[Scope(Value =ScopeFlag.Sington)]
public class GameManager : SingtonBehaviour<GameManager>
{

    private readonly List<IGameManager> _managers = new();
    private readonly IMessager _globalMessager = new Messager();
    public IMessager GlobalMessager { get { return _globalMessager; } }

    public override void Awake()
    {
        base.Awake();
        _managers.Add(new PlayerManager());
        _managers.Add(new ItemManager());
        //_managers.Add(new InventoryManager());
        _managers.Add(new ViewManager());

        _managers.ForEach(manager => { manager.Startup(); });

        var ctx = IDIContext.Get();
        ctx.Bind(typeof(A));
        ctx.Bind(typeof(B));
        var b = ctx.GetInstance<B>(typeof(B));
        b.MyA.SayHello();
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Q))
        {
            Application.Quit();
            Debug.Log("Quit");
        }
        _managers.ForEach(manager => { manager.Update(); });
    }

    public T GetManager<T>()
    {
        return (T)_managers.Find(manager => manager is T );

    }

}
