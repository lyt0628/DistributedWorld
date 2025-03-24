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
/// �[�������������ؓ؟��ʼ���[��ĸ���ģ�M
/// �K�ڳ�ʼ������ģ�K�ᣬ���d����
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


        // �����нű�ģ�黯֮����ܳ�ʼ��Lua������
        // ���ʹ�����Lua����ģ����뱣֤�����������ã� Ҳ����˵ֻ�ṩ���壬
        // ʹ��Լ���õ�API ���ǲ������ڶ���������
        DI.BindExternalInstance(QS.Api.Common.DINames.Lua_GameLogic, gameLogicLua);
        
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
        modules.Add(MotorGlobal.Instance);
        modules.Add(ExecutorGlobal.Instance);
        modules.Add(SkillGlobal.Instance);
        modules.Add(CharaGlobal.Instance);
        modules.Add(PlayerGlobal.Instance);
        modules.Add(AgentGlobal.Instance);
        modules.Add(UIGlobal.Instance);

        /// ��һ�°��߼��ɣ�
        /// ģ�鶼�������İ󶨵�Trunk�У������ӣ���Ҫ�������ص����Լ���취��֤�����Ŀ���
        /// ģ���ڲ��Լ��ͷ���ʹ�� DI ��
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
        /// ����ģ��
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

    // ���з����ṩ�Ĵ��a���N�]�ˣ������ŵ�����һ����F��
}
