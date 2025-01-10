using QS.Api.Control.Domain;
using QS.Control.Domain;
using QS.GameLib.Rx.Relay;

namespace QS.Api.Control.Service
{
    public interface ICharaTranslationProxyDataSource
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="data">Relay to nenerate CharaTranslationSnapshot</param>
        /// <returns>UUID  of controlled point</returns>
        string New(Relay<ICharaTranslationSnapshot> data);

        void Remove(string uuid);

        ICharaTranslationSnapshot Create();
    }
}
