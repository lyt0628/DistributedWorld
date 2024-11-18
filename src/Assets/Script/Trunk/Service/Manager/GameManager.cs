using GameLib;
using QS.API;
using System.Collections.Generic;
using UnityEngine;

using GameLib.DI;
using GameLib.Impl;
using QS.Impl;

public class GameManager : SingtonBehaviour<GameManager>
{
    private readonly List<IGameManager> _managers = new();
    private readonly IMessager _globalMessager = new Messager();
    public IMessager GlobalMessager { get { return _globalMessager; } }
    private readonly IDIContext _globalDIContext = IDIContext.New();
    public IDIContext GlobalDIContext { get { return _globalDIContext; } }

    public override void Awake()
    {
        base.Awake();
        _managers.Add(new PlayerManager());
        _managers.Add(new ItemManager());
        //_managers.Add(new InventoryManager());
        _managers.Add(new ViewManager());

        _managers.ForEach(manager => { manager.Startup(); });

        GlobalDIContext.Bind(typeof(A))
                        .Bind(typeof(B))
                        // Unity Service
                        .BindInstance(Camera.main)
                        // Gloal Setting
                        .Bind(typeof(GlobalPhysicSetting))
                        // Data Layer
                        .Bind(typeof(CharacterLocationData))
                        .Bind(typeof(PlayerInputData))
                        // Service Layer
                        .Bind(typeof(PlayerControllService))
                        .Bind(typeof(CharacterTranslationDTO));
        
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
