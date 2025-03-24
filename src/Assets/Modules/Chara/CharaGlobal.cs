



using GameLib.DI;
using QS.Api.Common;
using QS.Chara.Service;
using QS.Common;

namespace QS.Chara
{
    //在核心逻辑，必须和 Unity 是耦合的。直接使用Unity 的组件系统，不要使用自己的
    // 命令模式还是要使用，现在组件的作用只是为了放置数据和系统接口
    // 应当使用 系统.功能(数据) 的这种方式来调用逻辑才行
    // 除了数据计算以外，其他部分都是和 Unity 耦合的
    public class CharaGlobal : ModuleGlobal<CharaGlobal>
    {
        public CharaGlobal()
        {
            LoadOp = new UnitAsyncOp<IModuleGlobal>(this);
        }

        protected override AsyncOpBase<IModuleGlobal> LoadOp { get; }

        protected override void DoSetupBinding(IDIContext globalContext, IDIContext moduleContext)
        {
            globalContext
                // Deprecated
                .Bind<CharaInstrFacotry>()
                .Bind<JumpInstr>()
                .Bind<DodgeInstr>()
                .Bind<HitInstr>()
                .Bind<AimLockInstr>()
                .Bind<MoveInstr>(ScopeFlag.Prototype);
        }
    }
}