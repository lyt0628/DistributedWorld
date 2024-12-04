

using GameLib.DI;
using QS.Api;
using QS.Api.Setting;
using QS.GameLib.Pattern;
using QS.Impl;
using QS.Impl.Setting;

namespace QS.Common
{
    public class CommonGlobal : Sington<CommonGlobal>, IBindingProvider
    {
        internal IDIContext DI { get; } = IDIContext.New();

        public CommonGlobal() 
        {

            DI
                .Bind<GlobalPhysicSetting>()
                .BindInstance(LifecycleProvider.Instance);
        }
        public void ProvideBinding(IDIContext context)
        {
            context
                .BindExternalInstance(DI.GetInstance<ILifecycleProivder>())
                .BindExternalInstance(DI.GetInstance<IGlobalPhysicSetting>());
        }
    }
}
