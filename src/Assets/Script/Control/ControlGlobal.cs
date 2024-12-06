


using GameLib.DI;
using QS.Api.Control;
using QS.Api.Control.Service;
using QS.Api.Data;
using QS.Api.Service;
using QS.Common;
using QS.Control.Service;
using QS.GameLib.Pattern;
using QS.Impl.Data;
using QS.Impl.Service;
using QS.Impl.Service.DTO;
using QS.Impl.Setting;

namespace QS.Control
{
    /// <summary>
    /// Control ģ�鸺���ṩ���Ƶķ���, ��Ҫ��һЩ��ɿ��Ʒ�������������
    /// /// </summary>
    public class ControlGlobal : Sington<ControlGlobal>, IBindingProvider
    {
        internal IDIContext DI { get; } = IDIContext.New();
        public ControlGlobal() 
        {
            CommonGlobal.Instance.ProvideBinding(DI);

            DI.Bind<PlayerInstructionSetting>()
                .Bind<PlayerCharacterData>()
                .Bind<PlayerInputData>()
                .Bind<PlayerInstructionData>()
                .Bind<PlayerLocationData>()
                .Bind<CharacterTranslationDTO>()
                .Bind<CharacterInstructionFactory>()
                .Bind<ControlledPointDataSource>()
                .Bind<ControlledPointService>()
                .Bind<PlayerControllService>();
        }

        public void ProvideBinding(IDIContext context)
        {

             // ע�������ͻ�ȡ�Ľӿ�Ҫһ��
            context
                .BindExternalInstance(DI.GetInstance<IPlayerInstructionData>())
                .BindExternalInstance(DI.GetInstance<IPlayerLocationData>())
                .BindExternalInstance(DI.GetInstance<IPlayerCharacterData>())
                .BindExternalInstance(DI.GetInstance<IControlledPointDataSource_tag>())
                .BindExternalInstance(DI.GetInstance<IControlledPointService>())
                .BindExternalInstance(DI.GetInstance<IPlayerControllService>());
        }
    }
}