

using Cysharp.Threading.Tasks;
using GameLib.DI;
using QS.Api.Common;
using QS.Combat;
using QS.Common;
using QS.Executor;
using QS.GameLib.Pattern.Message;


namespace QS.UI
{
    /// <summary>
    /// UI 模塊是一個上層模塊，提供 Handler 作爲 API 
    /// 具體來說是，下層組件需要某些功能，但是這些功能在上層實現
    /// 那麼下層組件必須提供某些接口，這個接口的實現在上層
    /// 上層組件通過依賴注入，想下層返回這些實現
    /// Defaul
    /// 沒辦法，能接觸所有組件的只有Trunk，所以終端UI在那邊工作
    /// 這邊實現一些UI組件合適
    /// </summary>
    public class UIGlobal : ModuleGlobal<UIGlobal>
    {
        internal  IDIContext DI = IDIContext.New();
        public UIGlobal() 
        {
            /// 只有单个上下文才能保证单例
            DI.Bind<DialoguePannel>(nameof(DialoguePannel));
            DI.Bind<DefaultUIStack>();
            CommonGlobal.Instance.ProvideBinding(DI);
            CombatGlobal.Instance.ProvideBinding(DI);
            ExecutorGlobal.Instance.ProvideBinding(DI);

            DI.BindExternalInstance(DINames.UI_GLOBAL_MESSAGER, new Messager());
        }
        
        protected override IDIContext DIContext => DI;


        public override void ProvideBinding(IDIContext context)
        {
            // FIX: 模块间传递绑定必须先获取实例
            context.BindExternalInstance(DI.GetInstance<DialoguePannel>());
            context.BindExternalInstance(DI.GetInstance<IUIStack>());
        }
    }
}