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
    /// �[�������������ؓ؟��ʼ���[��ĸ���ģ�M
    /// �K�ڳ�ʼ������ģ�K�ᣬ���d����
    /// </summary>
    public class TrunkGlobal : SingtonBehaviour<TrunkGlobal>, ITrunkGlobal
    {
        public IDIContext Context { get; } = IDIContext.New();
        readonly LuaEnv luaEnv = new();


        readonly List<IModuleGlobal> modules = new();

        /// <summary>
        /// �� Awake �׶�ͬ���ؽ��а󶨣��Ҳ�ϣ�������첽��
        /// </summary>
        public override void Awake()
        {
            base.Awake();
            /// ������˳��Ĺ̶��ģ�����д�������򣬾��ֶ���������˳���
            modules.Add(DepsGlobal.Instance); //
            modules.Add(CommonGlobal.Instance); //
            modules.Add(CombatGlobal.Instance); //
            modules.Add(WorldItemGlobal.Instance); //
            modules.Add(CharaGlobal.Instance); //
            modules.Add(SkillGlobal.Instance); // 
            modules.Add(PlayerGlobal.Instance); // x
            modules.Add(AgentGlobal.Instance); //
            modules.Add(UIGlobal.Instance); // 

            /// �������^�}�s���Ҳ�ϣ�����ρ�l���}�����韩
            InitBinding();
            var player = Context.GetInstance<IPlayer>();
            player.ActiveChara = playerChara;
        }
        public Character playerChara;

        /// <summary>
        /// Start �׶�����ģ�飬����׶���Ҫ������Դ����˱������첽��
        /// </summary>
        async void Start()
        {


            /// �󶨳�ʼ����Ϻ�������ģ��
            StartCoroutine(InitModules());
            await UniTask.WhenAll(modules.Select(m => m.LoadHandle.Task.AsUniTask()));

            //var uiStack = GetInstance<IUIStack>();
            //uiStack.Add(GetInstance<MainHUD>());

        }

        void InitBinding()
        {

            // �����нű�ģ�黯֮����ܳ�ʼ��Lua������
            // ���ʹ�����Lua����ģ����뱣֤�����������ã� Ҳ����˵ֻ�ṩ���壬
            // ʹ��Լ���õ�API ���ǲ������ڶ���������

            Context.BindExternalInstance(this);

            /// ��һ�°��߼��ɣ�
            /// ģ�鶼�������İ󶨵�Trunk�У������ӣ���Ҫ�������ص����Լ���취��֤�����Ŀ���
            /// ģ���ڲ��Լ��ͷ���ʹ�� Context ��
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
            /// ����ģ��
            foreach (var module in modules)
            {
                // ��������һ����첽Handle ����
                module.LoadAsync();
            }

            
            // ����ʱ������Ҫ��ϸ����

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