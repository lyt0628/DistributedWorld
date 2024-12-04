


using GameLib.DI;
using QS.Api.Data;
using QS.Api.Service;
using QS.Common;
using QS.GameLib.Pattern;
using QS.Impl.Data;
using QS.Impl.Service;
using QS.Impl.Service.DTO;
using QS.Impl.Setting;

namespace QS.Control
{
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
                .Bind<PlayerControllService>();
        }

        public void ProvideBinding(IDIContext context)
        {
            context
                .BindExternalInstance(DI.GetInstance<IPlayerCharacterData>())
                .BindExternalInstance(DI.GetInstance<IPlayerControllService>());
        }
    }
}