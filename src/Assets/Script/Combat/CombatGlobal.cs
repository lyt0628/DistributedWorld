

using GameLib.DI;
using QS.Api.Combat.Service;
using QS.Api.Common;
using QS.Combat.Service;
using QS.Common;

namespace QS.Combat
{

    public class CombatGlobal : ModuleGlobal<CombatGlobal>
    {
        internal IDIContext DI = IDIContext.New();
        protected override IDIContext DIContext => DI;

        public CombatGlobal() 
        {
            CommonGlobal.Instance.ProvideBinding(DI);

            DI
                .Bind<BuffFactory>()
                .Bind<AttackFactory>();
        }


        public override void ProvideBinding(IDIContext context)
        {
            context
                .BindExternalInstance(DI.GetInstance<IBuffFactory>())
                .BindExternalInstance(DI.GetInstance<IAttackFactory>());
        }
    }
}