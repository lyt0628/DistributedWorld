

using GameLib.DI;
using QS.Api.Common;
using QS.Common;


namespace QS.UI
{
    /// <summary>
    /// UI 模塊是一個上層模塊，提供 Handler 作爲 API 
    /// </summary>
    class UIGlobal : ModuleGlobal<UIGlobal>
    {
        internal  IDIContext DI = IDIContext.New();
        public UIGlobal() 
        {
            CommonGlobal.Instance.ProvideBinding(DI);
        }

        protected override IDIContext DIContext => DI;

        public override void ProvideBinding(IDIContext context)
        {
        }
    }
}