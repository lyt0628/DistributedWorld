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
using QS.Motor;
using QS.Executor;
using QS.GameLib.Pattern;
using QS.Inventory;
using QS.PlayerControl;
using QS.Skill;
using QS.UI;
using QS.WorldItem;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AddressableAssets;
using UnityEngine.ResourceManagement.AsyncOperations;

/// <summary>
/// �[�������������ؓ؟��ʼ���[��ĸ���ģ�M
/// �K�ڳ�ʼ������ģ�K�ᣬ���d����
/// </summary>
public class TrunkGlobal : SingtonBehaviour<TrunkGlobal>, ITrunkGlobal
{
    internal IDIContext DI { get; } = IDIContext.New();
    readonly List<IModuleGlobal> modules = new();


    public override void Awake()
    {
        base.Awake();
        /// �������^�}�s���Ҳ�ϣ�����ρ�l���}�����韩
        InitBinding();
        StartCoroutine(InitModules());
    }

    private void InitBinding()
    {
        modules.Add(DepsGlobal.Instance);
        modules.Add(CommonGlobal.Instance);
        modules.Add(CombatGlobal.Instance);
        modules.Add(WorldItemGlobal.Instance);
        modules.Add(InventoryGlobal.Instance);
        modules.Add(MotorGlobal.Instance);
        modules.Add(ExecutorGlobal.Instance);
        modules.Add(SkillGlobal.Instance);
        modules.Add(CharaGlobal.Instance);
        modules.Add(PlayerControlGlobal.Instance);
        modules.Add(AgentGlobal.Instance);
        modules.Add(UIGlobal.Instance);

        modules.ForEach(m =>
        {
            m.ProvideBinding(DI);
            m.ReviceBinding(this);
        });

        DI
          .BindInstance(TrunkDINames.Trunk_GLOBAL, this)
          .Bind<DefaultCombator>(ScopeFlag.Prototype)
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


    // ���з����ṩ�Ĵ��a���N�]�ˣ������ŵ�����һ����F��
}
