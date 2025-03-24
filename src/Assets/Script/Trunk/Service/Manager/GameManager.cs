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
        // ��Щ���ں���ʵ�ֲ�� DI������ʱ��Ҫ�ֿ�
        // �ֲ������� �������Ӳ���Ч��, ʵ�ֳ��򼯷���,
        // ��� ʹ�� ͳһ�� DI������,���Խ����� �����Ը��� ����

        // GameLib ����������������, ��Ҫ�����������Ⱦ��ע��
        GlobalDIContext
            .Bind<DefaultPipelineConext>(ScopeFlag.Prototype);
        // Impl Layer
        GlobalDIContext
            .BindInstance(LifecycleProvider.Instance) // �������о��ܳ���,�������ŷŵ�GameLib
                                                      // Gloal Setting
            .Bind<GlobalPhysicSetting>()
            .Bind<PlayerInstructionSetting>()
            // Data Layer
            .Bind<PlayerLocationData>()
            .Bind<PlayerInputData>()
            .Bind<PlayerCharacterData>()
            .Bind<WorldItemDataLoader>()
            .Bind<ItemBreedSotre>() // ��Щ�಻����Ҫ���⹫����
            .Bind<ItemSotre>() // �����ֳ������ĺ�,��ЩҪλ���ڲ�
            .Bind<Inventory>() // Ȼ��ʹ�� SPI, ע��ΪManager����GameManager����
                               // Gata Gateway Layer
            .Bind<WeaponBreedRepo>()
            .Bind<WorldWeaponRepo>()
            .Bind<PlayerItemRepo>()
            .Bind<WorldItemRepoFacade>() // ���
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

    // ʲôʱ���ʹ�� SPI,������ʲôʱ����ʹ�� DI
    // ��ʵ����ʱ, ʵ�� ��������ʱ�ı�Ļ� ����ʹ��DI, ��Ϊ DI������ռ
    public T GetManager<T>()
    {
        return (T)_managers.Find(manager => manager is T);
    }

}
