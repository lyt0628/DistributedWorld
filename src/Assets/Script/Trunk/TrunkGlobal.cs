using GameLib.DI;
using KeraLua;
using Newtonsoft.Json;
using QS.Api;
using QS.Chara;
using QS.Combat;
using QS.Combat.Domain;
using QS.Common;
using QS.Control;
using QS.Executor;
using QS.GameLib.Pattern;
using QS.Inventory;
using QS.PlayerControl;
using QS.QLua;
using QS.Skill;
using QS.WorldItem;
using UnityEngine;

public class TrunkGlobal : SingtonBehaviour<TrunkGlobal>
{
    internal IDIContext DI { get; } = IDIContext.New();
    

    public override void Awake()
    {
        base.Awake();
        DepsGlobal.Instance.ProvideBinding(DI);
        QLuaGlobal.Instance.ProvideBinding(DI);
        CommonGlobal.Instance.ProvideBinding(DI);
        CombatGlobal.Instance.ProvideBinding(DI);
        WorldItemGlobal.Instance.ProvideBinding(DI);
        InventoryGlobal.Instance.ProvideBinding(DI);
        ControlGlobal.Instance.ProvideBinding(DI);
        ExecutorGlobal.Instance.ProvideBinding(DI);
        SkillGlobal.Instance.ProvideBinding(DI);
        CharaGlobal.Instance.ProvideBinding(DI);
        PlayerControlGlobal.Instance.ProvideBinding(DI);

        DI
          .BindInstance(TrunkDINames.Trunk_GLOBAL, this)
          .Bind<DefaultCombator>(ScopeFlag.Prototype)
          .Bind<HpUI>();

        DI.Inject(this);
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


}
