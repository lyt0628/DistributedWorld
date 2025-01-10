

using GameLib.DI;
using QS.Api.Common;
using QS.Common;


namespace QS.UI
{
    /// <summary>
    /// UI ģ�K��һ���ό�ģ�K���ṩ Handler ���� API 
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