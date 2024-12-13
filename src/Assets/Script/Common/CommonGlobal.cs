

using GameLib.DI;
using QS.Api;
using QS.Api.Common;
using QS.Api.Common.Util.Detector;
using QS.Api.Setting;
using QS.Common.Util.Detector;
using QS.GameLib.Pattern;
using QS.GameLib.Pattern.Message;
using QS.Impl;
using QS.Impl.Setting;

namespace QS.Common
{
    public class CommonGlobal : ModuleGlobal<CommonGlobal>
    {
        internal IDIContext DI { get; } = IDIContext.New();

        protected override IDIContext DIContext => DI;

        public CommonGlobal() 
        {
            DI
                // GameLib 
                .Bind<Messager>(DINames.GameLib_Message_Messager, ScopeFlag.Prototype)
                //
                .Bind<GlobalPhysicSetting>()
                .BindInstance(LifecycleProvider.Instance)
                .BindExternalInstance(DINames.MsgBus, new Messager())
                .BindExternalInstance(new DetectorFactory());
        }

        public override void ProvideBinding(IDIContext context)
        {
            context
                .BindExternalInstance(DINames.CommonGlobal, this)
                // Provide Bindings from GameLib 如果不想获取全部绑定, 就通过单独的GetInstance方法拿依赖
                .Bind<Messager>(DINames.GameLib_Message_Messager, ScopeFlag.Prototype)
                // Util
                .BindExternalInstance(DI.GetInstance<IDetectorFactory>())               
                // Global Message Bus
                .BindExternalInstance(DINames.MsgBus,
                                      DI.GetInstance(DINames.MsgBus, typeof(Messager)))
                .BindExternalInstance(DI.GetInstance<ILifecycleProivder>())
                .BindExternalInstance(DI.GetInstance<IGlobalPhysicSetting>());
        }
      
    }
}
