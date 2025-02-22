using GameLib.DI;
using Newtonsoft.Json;
using QS.Agent;
using QS.Api;
using QS.Api.Common;
using QS.Api.WorldItem.Service;
using QS.Chara;
using QS.Combat;
using QS.Combat.Domain;
using QS.Common;
using QS.Motor;
using QS.Executor;
using QS.GameLib.Pattern;
using QS.Player;
using QS.Skill;
using QS.UI;
using QS.WorldItem;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AddressableAssets;
using UnityEngine.ResourceManagement.AsyncOperations;
using NLua;
using UnityEngine.Events;
using System;
using QS.Trunk.UI;

/// <summary>
/// 遊戲的主控制器，負責初始化遊戲的各個模組
/// 並在初始化各個模塊後，加載場景
/// </summary>
public class TrunkGlobal : SingtonBehaviour<TrunkGlobal>, ITrunkGlobal
{
    internal IDIContext DI { get; } = IDIContext.New();

    public UnityEvent OnReady { get; } = new();

    readonly List<IModuleGlobal> modules = new();

    readonly Lua gameLogicLua = new();

    public override void Awake()
    {
        base.Awake();


        // 再所有脚本模块化之后才能初始化Lua环境，
        // 因此使用这个Lua的子模块必须保证不会立即调用， 也就是说只提供定义，
        // 使用约定好的API 但是不会用在顶级作用域
        DI.BindExternalInstance(QS.Api.Common.DINames.Lua_GameLogic, gameLogicLua);
        
        /// 綁定比較複雜，我不希望遇上併發問題，很麻煩
        InitBinding();
        StartCoroutine(InitModules());
    }

    private void InitBinding()
    {
        modules.Add(DepsGlobal.Instance);
        modules.Add(CommonGlobal.Instance);
        modules.Add(CombatGlobal.Instance);
        modules.Add(WorldItemGlobal.Instance);
        modules.Add(MotorGlobal.Instance);
        modules.Add(ExecutorGlobal.Instance);
        modules.Add(SkillGlobal.Instance);
        modules.Add(CharaGlobal.Instance);
        modules.Add(PlayerGlobal.Instance);
        modules.Add(AgentGlobal.Instance);
        modules.Add(UIGlobal.Instance);

        /// 换一下绑定逻辑吧！
        /// 模块都把上下文绑定到Trunk中，老样子，需要非懒加载的类自己想办法保证上下文可用
        /// 模块内部自己就放弃使用 DI 了
        modules.ForEach(m =>
        {
            m.ProvideBinding(DI);
            m.ReviceBinding(this);
        });
        SkillGlobal.Instance.ReviceBinding(DI.GetInstance<Lua>(QS.Api.Common.DINames.Lua_GameLogic));

        DI
          .Bind<InventoryUI>()
          .Bind<MainHUD>();

        DI.Inject(this);
    }

    public T GetInstance<T>()
    {
        return DI.GetInstance<T>();
    }

    public T GetInstance<T>(string name)
    {
        return DI.GetInstance<T>(name);
    }



    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Q))
        {
            Application.Quit();
            Debug.Log("Quit");
        }


    }

    IEnumerator InitModules()
    {
        /// 加载模块
        foreach (var module in modules)
        {
            module.Initialize();
        }
        yield return null;

        int totalModules = modules.Count;
        int initializedModules = 0;
        while(initializedModules < totalModules)
        {
            int lastInitializedModules = initializedModules;
            initializedModules = 0;
            foreach (var module in modules)
            {
                if (module.ResourceStatus == ResourceInitStatus.Started)
                {
                    initializedModules++;
                }
            }
            if(initializedModules > lastInitializedModules)
            {
                Debug.Log($"Progress: {initializedModules} / {totalModules}");
            }
            yield return null;
        }

        InitGameLogicLua();

        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Locked;

        OnReady.Invoke();
        Debug.Log("All modules initialized");
    }
    void InitGameLogicLua()
    {
        gameLogicLua["DebugLog"] = new Action<object>(Debug.Log);
        gameLogicLua["DebugLogWarning"] = new Action<object>(Debug.LogWarning);
        gameLogicLua["DebugLogError"] = new Action<object>(Debug.LogError);
        gameLogicLua["playerData"] = PlayerGlobal.Instance.GetInstance<IPlayerData>();

    }

    // 執行服務提供的代碼怎麼沒了，把它放到另外一個類實現吧
}
