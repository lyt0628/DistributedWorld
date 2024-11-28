using GameLib.DI;
using GameLib.Impl;
using QS.Api;
using QS.Domain.Combat;
using QS.GameLib.Pattern;
using QS.GameLib.Pattern.Message;
using QS.GameLib.Pattern.Pipeline;
using QS.Impl;
using QS.Impl.Data;
using QS.Impl.Data.Gateway;
using QS.Impl.Data.Gateway.Facade;
using QS.Impl.Data.Store;
using QS.Impl.Service;
using QS.Impl.Service.DTO;
using QS.Impl.Setting;
using System.Collections.Generic;
using UnityEngine;

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
        GlobalDIContext.BindInstance(DINames.SINGLE_GAME_MANAGER, this);
        // 这些层在后面实现层次 DI上下文时都要分开
        // 分层上下文 可以增加查找效率, 实现程序集分离,
        // 如果 使用 统一的 DI上下文,来对接依赖 还可以隔绝 依赖

        // GameLib 当做三方依赖处理, 不要在里面存在污染的注解
        GlobalDIContext
            .Bind<DefaultPipelineConext>(ScopeFlag.Prototype);
        // Impl Layer
        GlobalDIContext
            .BindInstance(LifecycleProvider.Instance) // 这个组件感觉很抽象,可以试着放到GameLib
                                                      // Gloal Setting
            .Bind<GlobalPhysicSetting>()
            .Bind<PlayerInstructionSetting>()
            // Data Layer
            .Bind<PlayerLocationData>()
            .Bind<PlayerInputData>()
            .Bind<PlayerCharacterData>()
            .Bind<WorldItemDataLoader>()
            .Bind<ItemBreedSotre>() // 这些类不是想要对外公布的
            .Bind<ItemSotre>() // 做出分成上下文后,这些要位于内部
            .Bind<Inventory>() // 然后使用 SPI, 注册为Manager来到GameManager调用
                               // Gata Gateway Layer
            .Bind<WeaponBreedRepo>()
            .Bind<WorldWeaponRepo>()
            .Bind<PlayerItemRepo>()
            .Bind<WorldItemRepoFacade>() // 外观
            .Bind<PlayerItemRepofacade>()
            // Service Layer
            .Bind<PlayerControllService>()
            .Bind<PlayerInstructionData>()
            .Bind<CharacterTranslationDTO>(ScopeFlag.Prototype);

        // Trunk Layer
        GlobalDIContext
            //.Bind(typeof(WeaponRefineService))
            .Bind<DefaultCombator>(ScopeFlag.Prototype);

        GlobalDIContext.Inject(this);

        GlobalDIContext.GetInstance<WorldItemDataLoader>();

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
        return (T)_managers.Find(manager => manager is T);
    }

}
