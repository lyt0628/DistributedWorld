


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
    /// Control ģ�鸺���ṩ���Ƶķ���, ��Ҫ��һЩ��ɿ��Ʒ�������������
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

            // ע�������ͻ�ȡ�Ľӿ�Ҫһ��
            context
                 .BindExternalInstance(DI.GetInstance<ICharaTranslationProxyDataSource_tag>())
                 .BindExternalInstance(DI.GetInstance<ICharaTranslationControl>());
        }

    }
}