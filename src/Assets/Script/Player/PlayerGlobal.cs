using GameLib.DI;
using QS.Api.Common;
using QS.Chara;
using QS.Common;
using QS.Executor;
using QS.GameLib.Pattern;
using QS.GameLib.Pattern.Message;
using QS.GameLib.View;
using QS.Impl.Setting;
using QS.PlayerControl;
using QS.Skill;
using QS.UI;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace QS.Player
{
    public class PlayerGlobal : ModuleGlobal<PlayerGlobal>
    {
        internal IDIContext DI = IDIContext.New();
        [Injected]
        readonly IPlayerData playerChara;
        public PlayerGlobal() 
        {
            CommonGlobal.Instance.ProvideBinding(DI);
            ExecutorGlobal.Instance.ProvideBinding(DI);
            CharaGlobal.Instance.ProvideBinding(DI);
            SkillGlobal.Instance.ProvideBinding(DI);

            var playerControls = new PlayerControls();
            playerControls.Enable();
            DI
                // InputSystem
                .BindExternalInstance(playerControls)
                .Bind<DefaultInventory>()
                .Bind<PlayerData>()
                .Bind<PlayerControlCallbacks>();
            DI.Inject(this);
        }

        protected override IDIContext DIContext => DI;

        public override void Initialize()
        {
            var uiGlobalMessager = UIGlobal.Instance.GetInstance<IMessager>(QS.UI.DINames.UI_GLOBAL_MESSAGER);
            var playerControls = DI.GetInstance<PlayerControls>();
            uiGlobalMessager.AddListener(AbstractView.MSG_BLOCK_PLAYER_CONTROL_UI_SHOW, (_) =>
            {
                playerControls.Player.Disable();
                playerChara.ActivedCharacter.Frozen = true;
                Debug.Log("Disable Player Control");
            });
            uiGlobalMessager.AddListener(AbstractView.MSG_BLOCK_PLAYER_CONTROL_UI_HIDE, (_) =>
            {
                playerControls.Player.Enable();
                playerChara.ActivedCharacter.Frozen = false;
            });
            
            
            base.Initialize();
        }

        public override void ProvideBinding(IDIContext context)
        {
            context
                .BindExternalInstance(DI.GetInstance<PlayerControls>())
                .BindExternalInstance(DI.GetInstance<IInventory>())
                .BindExternalInstance(DI.GetInstance<IPlayerData>());
        }

    }
 

}