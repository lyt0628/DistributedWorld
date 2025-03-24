using GameLib.DI;
using QS.Api.Common;
using QS.Common;
using QS.Player.Instrs;


namespace QS.Player
{
    public class PlayerGlobal : ModuleGlobal<PlayerGlobal>
    {
        public PlayerGlobal()
        {
            LoadOp = new PlayerModuleLoadOp(this);
        }

        protected override AsyncOpBase<IModuleGlobal> LoadOp { get; }
        private class PlayerModuleLoadOp : AsyncOpBase<IModuleGlobal>
        {
            readonly PlayerGlobal playerGlobal;
            public PlayerModuleLoadOp(PlayerGlobal playerGlobal)
            {
                this.playerGlobal = playerGlobal;

            }

            protected override void Execute()
            {

                //BindLuaEnv();


                Complete(playerGlobal);
            }

        }


        /// 初始化绑定不要是异步的
        protected override void DoSetupBinding(IDIContext globalContext, IDIContext moduleContext)
        {
            var player = new Player();
            globalContext
               .BindExternalInstance(player)
               .Bind<InteractInstr>()
               .Bind<CarmeraFocusInstr>()
               .Bind<DefaultInventory>();

            //moduleContext.Bind<PlayerControlCallbacks>();

            Inject(this);
        }



        //[Injected]
        //readonly IPlayer playerChara;
        //protected Task DoInit()
        //{

        //    var uiGlobalMessager = new Messager();

        //    var playerControls = GetInstance<PlayerControls>();
        //    //FIX: 不可重入
        //    //uiGlobalMessager.AddListener(BaseDocument.MSG_BLOCK_PLAYER_CONTROL_UI_SHOW, (_) =>
        //    //{
        //    //    playerControls.Player.Disable();
        //    //    playerChara.ActiveChara.frozen = true;
        //    //    Debug.Log("Disable Player Control");
        //    //});
        //    //uiGlobalMessager.AddListener(BaseDocument.MSG_BLOCK_PLAYER_CONTROL_UI_HIDE, (_) =>
        //    //{
        //    //    playerControls.Player.Enable();
        //    //    playerChara.ActiveChara.frozen = false;
        //    //});

        //    return base.DoInit();
        //}



    }


}