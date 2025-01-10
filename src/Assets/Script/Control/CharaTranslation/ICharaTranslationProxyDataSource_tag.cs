

using QS.Api.Control.Service;
using QS.Control.Domain;
using QS.GameLib.Rx.Relay;

namespace QS.Control.Service
{
    interface ICharaTranslationProxyDataSource_tag : ICharaTranslationProxyDataSource
    {
        public Relay<ICharaTranslationProxy> Get(string uuid);
    }
}