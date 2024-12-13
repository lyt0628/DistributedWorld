using GameLib.DI;
using QS.Api.Common;
using QS.Api.Data;
using QS.Common;
using QS.GameLib.Pattern;
using QS.Impl.Data;
using QS.Impl.Setting;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace QS.PlayerControl
{
    public class PlayerControlGlobal : ModuleGlobal<PlayerControlGlobal>
    {
        internal IDIContext DI = IDIContext.New();
        public PlayerControlGlobal() 
        {
            DI
                .Bind<PlayerInstructionSetting>()
                .Bind<PlayerCharacterData>()
                .Bind<PlayerInputData>()
                .Bind<PlayerInstructionData>()
                .Bind<PlayerLocationData>();

        }

        protected override IDIContext DIContext => DI;

        public override void ProvideBinding(IDIContext context)
        {
            context
                .BindExternalInstance(DI.GetInstance<IPlayerInstructionData>())
                .BindExternalInstance(DI.GetInstance<IPlayerLocationData>())
                .BindExternalInstance(DI.GetInstance<IPlayerCharacterData>());

        }

    }

}