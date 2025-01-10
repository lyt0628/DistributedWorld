using GameLib.DI;
using KeraLua;
using Newtonsoft.Json;
using QS.Agent;
using QS.Api;
using QS.Api.Common;
using QS.Api.WorldItem.Service;
using QS.Chara;
using QS.Combat;
using QS.Combat.Domain;
using QS.Common;
using QS.Control;
using QS.Executor;
using QS.GameLib.Pattern;
using QS.Inventory;
using QS.PlayerControl;
using QS.Skill;
using QS.WorldItem;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AddressableAssets;
using UnityEngine.ResourceManagement.AsyncOperations;

/// <summary>
/// 遊戲的主控制器，負責初始化遊戲的各個模組
/// 並在初始化各個模塊後，加載場景
/// </summary>
public class TrunkGlobal : SingtonBehaviour<TrunkGlobal>, IInstanceProvider
{
    internal IDIContext DI { get; } = IDIContext.New();
    readonly List<IResourceInitializer> modules = new();


    public override void Awake()
    {
        base.Awake();
        DepsGlobal.Instance.ProvideBinding(DI);
        modules.Add(DepsGlobal.Instance);
        CommonGlobal.Instance.ProvideBinding(DI);
        modules.Add(CommonGlobal.Instance);
        CombatGlobal.Instance.ProvideBinding(DI);
        modules.Add(CombatGlobal.Instance);
        WorldItemGlobal.Instance.ProvideBinding(DI);
        modules.Add(WorldItemGlobal.Instance);
        InventoryGlobal.Instance.ProvideBinding(DI);
        modules.Add(InventoryGlobal.Instance);
        ControlGlobal.Instance.ProvideBinding(DI);
        modules.Add(ControlGlobal.Instance);
        ExecutorGlobal.Instance.ProvideBinding(DI);
        modules.Add(ExecutorGlobal.Instance);
        SkillGlobal.Instance.ProvideBinding(DI);
        modules.Add(SkillGlobal.Instance);
        CharaGlobal.Instance.ProvideBinding(DI);
        modules.Add(CharaGlobal.Instance);
        PlayerControlGlobal.Instance.ProvideBinding(DI);
        modules.Add(PlayerControlGlobal.Instance);
        AgentGlobal.Instance.ProvideBinding(DI);
        modules.Add(AgentGlobal.Instance);

       StartCoroutine(InitModules());

        DI
          .BindInstance(TrunkDINames.Trunk_GLOBAL, this)
          .Bind<DefaultCombator>(ScopeFlag.Prototype)
          .Bind<HpUI>();

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

        if (Input.GetKeyDown(KeyCode.R))
        {
            GameObject.Find("BIG").AddComponent<CLogBehaviour>();
          var c =  GameObject.Find("BIG").GetComponent<CLogBehaviour>();
          Destroy(c);
        }

    }

    IEnumerator InitModules()
    {
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
        Debug.Log("All modules initialized");
    }


    // 執行服務提供的代碼怎麼沒了，把它放到另外一個類實現吧
}
