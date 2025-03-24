

using GameLib.DI;
using QS.Api.Common;
using QS.Combat.Domain;
using QS.Common;
using QS.GameLib.Pattern;
using QS.PlayerControl;

namespace QS.UI
{
    class UIGlobal : ModuleGlobal<UIGlobal>
    {
        internal  IDIContext DI = IDIContext.New();
        public UIGlobal() 
        {
            CommonGlobal.Instance.ProvideBinding(DI);
            PlayerControlGlobal.Instance.ProvideBinding(DI);
        }

        protected override IDIContext DIContext => DI;

        public override void ProvideBinding(IDIContext context)
        {
        }
    }
}