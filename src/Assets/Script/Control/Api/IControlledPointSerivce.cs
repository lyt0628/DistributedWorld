


using QS.Api.Control.Service.DTO;
using QS.GameLib.Rx.Relay;

namespace QS.Api.Control.Service
{
    public interface ICharaTranslationControl
    {
        Relay<ICharaTranslation> GetTranslation(string uuid);
    }
}