using GameLib;
using GameLib.View;
using QS.API;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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
        _managers.Add(new InventoryManager());
        _managers.Add(new ViewManager());

        _managers.ForEach(manager => { manager.Startup(); });
    }

    void Update()
    {
        _managers.ForEach(manager => { manager.Update(); });
    }

    public T GetManager<T>()
    {
        return (T)_managers.Find(manager => manager is T );

    }

}
