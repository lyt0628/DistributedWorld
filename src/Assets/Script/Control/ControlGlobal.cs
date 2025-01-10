


using GameLib.DI;
using QS.Api.Common;
using QS.Api.Control;
using QS.Api.Control.Service;
using QS.Common;
using QS.Control.Service;
using QS.GameLib.Pattern;
using QS.Impl.Service;
using QS.Impl.Service.DTO;
using QS.Impl.Setting;

namespace QS.Control
{
    /// <summary>
    /// Control 模块负责提供控制的服务, 主要是一些完成控制服务的领域服务类
    /// /// </summary>
    public class ControlGlobal : ModuleGlobal<ControlGlobal>
    {
        internal IDIContext DI { get; } = IDIContext.New();

        protected override IDIContext DIContext => DI;

        public ControlGlobal() 
        {
            CommonGlobal.Instance.ProvideBinding(DI);

            DI
                .Bind<CharaTranslation>()
                .Bind<CharaTranslationProxyDataSource>()
                .Bind<CharaTranslationControl>();
        }

        public override void ProvideBinding(IDIContext context)
        {

            // 注意声明和获取的接口要一致
            context
                 .BindExternalInstance(DI.GetInstance<ICharaTranslationProxyDataSource_tag>())
                 .BindExternalInstance(DI.GetInstance<ICharaTranslationControl>());
        }

    }
}