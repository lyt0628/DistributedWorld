using Cysharp.Threading.Tasks;
using GameLib.DI;
using QS.Agent;
using QS.Api.Common;
using QS.Chara;
using QS.Chara.Domain;
using QS.Combat;
using QS.Common;
using QS.GameLib.Pattern;
using QS.Player;
using QS.Skill;
using QS.Trunk.Chara.Samurai;
using QS.Trunk.Player;
using QS.Trunk.UI;
using QS.UI;
using QS.WorldItem;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Reactive.Linq;
using UnityEngine;
using XLua;
namespace QS.Trunk
{

    /// <summary>
    /// 遊戲的主控制器，負責初始化遊戲的各個模組
    /// 並在初始化各個模塊後，加載場景
    /// </summary>
    public class TrunkGlobal : SingtonBehaviour<TrunkGlobal>, ITrunkGlobal
    {
        public IDIContext Context { get; } = IDIContext.New();
        readonly LuaEnv luaEnv = new();


        readonly List<IModuleGlobal> modules = new();

        /// <summary>
        /// 在 Awake 阶段同步地进行绑定，我不希望绑定是异步的
        /// </summary>
        public override void Awake()
        {
            base.Awake();
            /// 依赖的顺序的固定的，不想写拓扑排序，就手动调整加载顺序吧
            modules.Add(DepsGlobal.Instance); //
            modules.Add(CommonGlobal.Instance); //
            modules.Add(CombatGlobal.Instance); //
            modules.Add(WorldItemGlobal.Instance); //
            modules.Add(CharaGlobal.Instance); //
            modules.Add(SkillGlobal.Instance); // 
            modules.Add(PlayerGlobal.Instance); // x
            modules.Add(AgentGlobal.Instance); //
            modules.Add(UIGlobal.Instance); // 

            /// 綁定比較複雜，我不希望遇上併發問題，很麻煩
            InitBinding();
            var player = Context.GetInstance<IPlayer>();
            player.ActiveChara = playerChara;
        }
        public Character playerChara;

        /// <summary>
        /// Start 阶段启动模块，这个阶段需要加载资源，因此必须是异步的
        /// </summary>
        async void Start()
        {


            /// 绑定初始化完毕后，再启动模块
            StartCoroutine(InitModules());
            await UniTask.WhenAll(modules.Select(m => m.LoadHandle.Task.AsUniTask()));

            //var uiStack = GetInstance<IUIStack>();
            //uiStack.Add(GetInstance<MainHUD>());

        }

        void InitBinding()
        {

            // 再所有脚本模块化之后才能初始化Lua环境，
            // 因此使用这个Lua的子模块必须保证不会立即调用， 也就是说只提供定义，
            // 使用约定好的API 但是不会用在顶级作用域

            Context.BindExternalInstance(this);

            /// 换一下绑定逻辑吧！
            /// 模块都把上下文绑定到Trunk中，老样子，需要非懒加载的类自己想办法保证上下文可用
            /// 模块内部自己就放弃使用 Context 了
            modules.ForEach(m => m.SetupBinding(this));

            Context
                .Bind<KatanaLightAttackInstr>()
                .Bind<BowLightAttackInstr>()
                .Bind<SwitchWeaponInstr>()
                .Bind<LightAttackInstr>()
                .Bind<ViewNoteUI>()
                .Bind<InventoryUI>()
                .BindExternalInstance(luaEnv);
                //.Bind<DialogPanel>();

            var h_LuaModule = new LuaModuleLoadOp(luaEnv);
            h_LuaModule.Invoke();
            Context.BindExternalInstance(DINames.ASYNCOP_HANDLE_LOAD_LUA_MODULES, h_LuaModule.Handle);

            luaEnv.Global.Set("player", Context.GetInstance<IPlayer>());


            var uis = Context.GetInstance<IUIStack>();
            var h_MainHUD = new LuaUIDocumentLoadOp("Lua_UI_MainHUD");
            h_MainHUD.Invoke();
            h_MainHUD.Completed += (h) =>
            {
                uis.Push(h.Result);
            };

            var h_DialogPanel = new LuaUIDocumentLoadOp("Lua_UI_DialogPanel");
            h_DialogPanel.Invoke();
            h_DialogPanel.Completed += (h) =>
            {
                Context.BindExternalInstance(Trunk.DINames.UI_DIALOG_PANEL, h.Result);
            };

            Context.Inject(this);
        }

        public T GetInstance<T>()
        {
            return Context.GetInstance<T>();
        }


        void Update()
        {
            if (Input.GetKeyDown(KeyCode.Q))
            {
                Application.Quit();
                Debug.Log("Quit");
            }

            UpdateXLuaGCTick();
        }


        #region [[XLua GC Tick]]
        static float lastGCTime = 0;
        const float GCInterval = 1;//1 second 
        private void UpdateXLuaGCTick()
        {
            if (Time.time - lastGCTime > GCInterval)
            {
                luaEnv.Tick();
                lastGCTime = Time.time;
            }
        }
        #endregion


        IEnumerator InitModules()
        {
            /// 加载模块
            foreach (var module in modules)
            {
                // 现在这个家伙由异步Handle 来做
                module.LoadAsync();
            }

            
            // 加载时机还需要仔细推敲

            yield return null;

            int totalModules = modules.Count;
            int initializedModules = 0;
            while (initializedModules < totalModules)
            {
                int lastInitializedModules = initializedModules;
                initializedModules = 0;
                foreach (var module in modules)
                {
                    if (module.LoadHandle.IsDone == true)
                    {
                        initializedModules++;
                        //Debug.Log(module.GetType().Name);
                    }
                }
                if (initializedModules > lastInitializedModules)
                {
                    Debug.Log($"Progress: {initializedModules} / {totalModules}");
                }
                yield return null;
            }




            MiscUtil.HideCursor();
            Debug.Log("All modules initialized");
        }



    }

}