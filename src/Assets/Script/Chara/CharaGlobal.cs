



using GameLib.DI;
using QS.Api.Chara.Service;
using QS.Api.Common;
using QS.Chara.Service;
using QS.Combat;
using QS.Common;
using QS.Motor;
using QS.Executor;

namespace QS.Chara
{
    public class CharaGlobal : ModuleGlobal<CharaGlobal>
    {
        internal IDIContext DI = IDIContext.New();
        public CharaGlobal()
        {
            CommonGlobal.Instance.ProvideBinding(DI);
            MotorGlobal.Instance.ProvideBinding(DI);
            CombatGlobal.Instance.ProvideBinding(DI);
            ExecutorGlobal.Instance.ProvideBinding(DI);

            DI
                .Bind<CharaInstrFacotry>()
                .Bind<CharaAblityFactory>();
        }

        protected override IDIContext DIContext => DI;

        public override void ProvideBinding(IDIContext context)
        {
            context
                .BindExternalInstance(DI.GetInstance<ICharaInsrFactory>())
                .BindExternalInstance(DI.GetInstance<ICharaAblityFactory>());
        }
    }
}